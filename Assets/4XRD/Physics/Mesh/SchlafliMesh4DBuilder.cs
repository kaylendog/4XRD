using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace _4XRD.Physics.Mesh
{
    /// <summary>
    ///     Builds 4D meshes from 3-element Schlafli symbology.
    /// </summary>
    public class SchlafliMesh4DBuilder : IPrimitiveMesh4DBuilder
    {
        readonly int _p;
        readonly int _q;
        readonly int _r;

        public SchlafliMesh4DBuilder(int p, int q, int r)
        {
            _p = p;
            _q = q;
            _r = r;
        }

        public Mesh4D Build()
        {
            var mirrorNormals = GetMirrorNormals();
            var generatorCount = 4;
            var relations = GetRelationTable();

            var vertexTable = CosetTableBuilder.Build(generatorCount, relations, new[] { 1, 2, 3 });
            var edgeTable = CosetTableBuilder.Build(generatorCount, relations, new[] { 0, 2, 3 });
            var faceTable = CosetTableBuilder.Build(generatorCount, relations, new[] { 0, 1, 3 });
            var cellTable = CosetTableBuilder.Build(generatorCount, relations, new[] { 0, 1, 2 });

            var v0 = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);
            v0.y = -mirrorNormals[1].x * v0.x / mirrorNormals[1].y;
            v0.z = -mirrorNormals[2].y * v0.y / mirrorNormals[2].z;
            v0.w = -mirrorNormals[3].z * v0.z / mirrorNormals[3].w;
            v0.Normalize();

            var vertices = CosetTableBuilder.Fold(vertexTable,
                0,
                v0,
                (v, mirror) => v.Reflect(mirrorNormals[mirror]));


            return new Mesh4D();
        }


        /// <summary>
        ///     Return the mirror normals from the symbology.
        /// </summary>
        /// <returns></returns>
        Vector4[] GetMirrorNormals()
        {
            var output = new Vector4[4];

            output[0] = new Vector4(1.0f, 0.0f, 0.0f, 0.0f);

            output[1].x = Mathf.Cos(Mathf.PI / _p);
            output[1].y = Mathf.Sqrt(1.0f - Mathf.Pow(output[1].x, 2.0f));

            output[2].y = Mathf.Cos(Mathf.PI / _q) / output[1].y;
            output[2].z = Mathf.Sqrt(1.0f - Mathf.Pow(output[2].y, 2));

            output[3].z = Mathf.Cos(Mathf.PI / _r) / output[2].z;
            output[3].w = Mathf.Sqrt(1.0f - Mathf.Pow(output[3].z, 2));


            return output;
        }

        /// <summary>
        ///     Build the relation table for Todd-Coexter.
        /// </summary>
        /// <returns></returns>
        int[][] GetRelationTable()
        {
            return new[]
            {
                new[] { 0, 0 },
                new[] { 1, 1 },
                new[] { 2, 2 },
                new[] { 3, 3 },
                Enumerable.Repeat(new[] { 0, 1 }, _p)
                    .SelectMany(x => x)
                    .ToArray(),
                Enumerable.Repeat(new[] { 1, 2 }, _q)
                    .SelectMany(x => x)
                    .ToArray(),
                Enumerable.Repeat(new[] { 2, 3 }, _r)
                    .SelectMany(x => x)
                    .ToArray(),
                new[] { 0, 2, 0, 2 },
                new[] { 0, 3, 0, 3 },
                new[] { 1, 3, 1, 3 }
            };
        }
    }

    /// <summary>
    ///     Coset table containing columns x' in X' for all cosets of H.
    /// </summary>
    readonly struct CosetTableBuilder
    {
        readonly int _generatorCount;
        readonly List<int?[]> _table;
        readonly List<int> _live;

        public static int[][] Build(int generatorCount, int[][] relations, int[] subGenerators)
        {
            var table = new CosetTableBuilder();

            // fill in initial information for first coset
            foreach (var g in subGenerators)
            {
                table.ScanAndFill(0, new[] { g });
            }

            var i = 0;
            while (i < table._table.Count)
            {
                if (table._live[i] == i)
                {
                    foreach (var relation in relations)
                    {
                        table.ScanAndFill(i, relation);
                    }

                    for (var g = 0; g < generatorCount; g++)
                    {
                        if (table._table[i][g] == null)
                        {
                            table.Define(i, g);
                        }
                    }
                }
                i += 1;
            }

            // compress the resulting table
            var forward = new NativeHashMap<int, int>();
            var backward = new NativeHashMap<int, int>();
            var fresh = 0;

            for (var coset = 0; coset < table._table.Count; coset++)
            {
                if (table._live[coset] == coset)
                {
                    forward[coset] = fresh;
                    backward[fresh] = coset;
                    fresh += 1;
                }
            }

            var compressed = new List<int[]>();

            for (var j = 0; j < fresh; j++)
            {
                compressed.Add(table._table[backward[j]].Select(x => forward[(int)x]).ToArray());
            }

            return compressed.ToArray();
        }

        public static List<T> Fold<T>(int[][] table, int start, T initial, Func<T, int, T> f)
        {
            var result = Enumerable.Repeat(initial, table.Length).ToList();
            var queue = new Queue<int>();
            var seen = new HashSet<int>();

            seen.Add(start);

            while (queue.Count < 0)
            {
                var top = queue.Dequeue();

                for (var i = 0; i < table[top].Length; i++)
                {
                    var g = table[top][i];
                    if (seen.Contains(i))
                    {
                        continue;
                    }

                    result[i] = f(result[top], g);
                    queue.Enqueue(i);
                    seen.Add(i);
                }
            }

            return result;
        }

        public CosetTableBuilder(int generatorCount)
        {
            _generatorCount = generatorCount;
            _table = new List<int?[]>();
            _live = new List<int>();

            // intialise initial row
            _table.Add(Enumerable.Repeat<int?>(null, generatorCount).ToArray());
        }

        void Define(int coset, int g)
        {
            var fresh = _table.Count;
            _table.Add(Enumerable.Repeat<int?>(null, _generatorCount).ToArray());
            _table[coset][g] = fresh;
            _table[fresh][g] = coset;
            _live.Add(fresh);
        }

        int Rep(int coset)
        {
            // find fixed point
            var m = coset;
            while (m != _live[m])
            {
                m = _live[m];
            }

            // propagate
            var j = coset;
            while (j != _live[j])
            {
                var next = _live[j];
                _live[j] = m;
                j = next;
            }

            return m;
        }

        void Merge(Queue<int> queue, int a, int b)
        {
            var s = Rep(a);
            var t = Rep(b);

            if (s == t)
                return;

            // enforce order
            s = Mathf.Min(s, t);
            t = Mathf.Max(s, t);

            _live[t] = s;
            queue.Enqueue(t);
        }

        void Coincidence(int a, int b)
        {
            var queue = new Queue<int>();
            Merge(queue, a, b);

            while (queue.Count != 0)
            {
                var e = queue.Dequeue();
                for (var generatorIdx = 0; generatorIdx < _generatorCount; generatorIdx++)
                {

                    if (_table[e][generatorIdx] == null)
                    {
                        return;
                    }

                    var f = (int)_table[e][generatorIdx];
                    _table[f][generatorIdx] = null;

                    var ePrime = Rep(e);
                    var fPrime = Rep(f);

                    if (_table[ePrime][generatorIdx] != null)
                    {
                        Merge(queue, ePrime, (int)_table[ePrime][generatorIdx]);
                    }
                    else if (_table[fPrime][generatorIdx] != null)
                    {
                        Merge(queue, ePrime, (int)_table[fPrime][generatorIdx]);
                    }
                    else
                    {
                        _table[ePrime][generatorIdx] = fPrime;
                        _table[fPrime][generatorIdx] = ePrime;
                    }
                }
            }
        }

        void ScanAndFill(int coset, int[] word)
        {
            var f = coset;
            var b = coset;
            var i = 0;
            var j = word.Length - 1;

            while (true)
            {
                while (i <= j && _table[f][word[i]] != null)
                {
                    f = (int)_table[f][word[i]];
                    i += 1;
                }

                if (i > j)
                {
                    // check for coincidences
                    if (f != b)
                    {
                        Coincidence(f, b);
                    }
                    return;
                }

                while (j >= i && _table[b][word[j]] != null)
                {
                    b = (int)_table[b][word[j]];
                    j -= 1;
                }

                if (j < i)
                {
                    Coincidence(f, b);
                    return;
                }

                if (i == j)
                {
                    _table[f][word[i]] = b;
                    _table[b][word[i]] = f;
                }
                else
                {
                    Define(f, word[i]);
                }
            }
        }
    }
}
