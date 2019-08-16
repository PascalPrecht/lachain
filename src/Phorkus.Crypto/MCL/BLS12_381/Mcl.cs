﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Phorkus.Crypto.MCL.BLS12_381
{
    public static class Mcl
    {
        private static bool _initCalled;

        public static void Init()
        {
            if (_initCalled) return; // TODO: make singleton or whatsoever
            const int curveBls12381 = 5;
            const int compileTimeVar = 46;
            var error = MclImports.mclBn_init(curveBls12381, compileTimeVar);
            if (error != 0)
            {
                throw new InvalidOperationException("mclBn_init returned error " + error);
            }

            _initCalled = true;
        }

        public static G2 LagrangeInterpolateG2(Fr[] xs, G2[] ys)
        {
            if (xs.Length != ys.Length) throw new ArgumentException("arrays are unequal length");
            var res = new G2();
            MclImports.mclBn_G2LagrangeInterpolation(ref res, xs, ys, xs.Length);
            return res;
        }
        
        public static G1 LagrangeInterpolateG1(Fr[] xs, G1[] ys)
        {
            if (xs.Length != ys.Length) throw new ArgumentException("arrays are unequal length");
            var res = new G1();
            MclImports.mclBn_G1LagrangeInterpolation(ref res, xs, ys, xs.Length);
            return res;
        }

        public static GT Pairing(G1 x, G2 y)
        {
            var res = new GT();
            MclImports.mclBn_pairing(ref res, ref x, ref y);
            return res;
        }

//        public static Fr GetValue(IEnumerable<Fr> P, int at)
//        {
//            var res = Fr.FromInt(0);
//            var by = Fr.FromInt(at);
//            var cur = Fr.FromInt(1);
//
//            foreach (var coeff in P)
//            {
//                res += cur * coeff;
//                cur *= by;
//            }
//
//            return res;
//        }

        public static dynamic GetValue(IEnumerable<dynamic> P, int at, dynamic zero)
        {
            var res = zero;
            var by = Fr.FromInt(at);
            var cur = Fr.FromInt(1);

            foreach (var coeff in P)
            {
                res += coeff * cur;
                cur *= by;
            }
            return res;
        }
        
        
//        public static G2 GetValue(IEnumerable<G2> P, int at)
//        {
//            var res = G2.Zero;
//            var by = Fr.FromInt(at);
//            var cur = Fr.FromInt(1);
//
//            foreach (var coeff in P)
//            {
//                res += coeff * cur;
//                cur *= by;
//            }
//
//            return res;
//        }


        public static byte[] CalculateHash(G1[] gs)
        {
            var temp = new byte[0];
            foreach (var g in gs)
            {
                temp = temp.Concat(G1.ToBytes(g)).ToArray();
            }

            return temp.Keccak256();
        }
    }
}