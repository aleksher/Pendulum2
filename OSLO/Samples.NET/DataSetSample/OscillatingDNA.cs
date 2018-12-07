﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Research.Oslo;

namespace DataSetSample
{
    class OscillatingDNA
    {
        // This example relies on the use of Microsoft Research Dataset Viewer, which is available 
        // at http://research.microsoft.com/en-us/um/cambridge/groups/science/tools/datasetviewer/datasetviewer.htm
        public OscillatingDNA()
        {
        }

        public void solve(ref List<double> tsim, ref List<double>[] xsim)
        {
            var plotLocs = new List<List<int>>();
            plotLocs.Add(new List<int> { 76, 128, 143 });
            plotLocs.Add(new List<int> { 44, 82, 97 });
            plotLocs.Add(new List<int> { 40, 50, 56 });

            double tfinal = 500000.0;

            // Ode options and initial conditions, then solve
            var opts = new Options { RelativeTolerance = 1e-4 };
            var x0 = oscillating_ics();

            IEnumerable<SolPoint> sol;
           
            sol = Ode.GearBDF(0.0, tfinal, x0, (t, x) => derivs(t, x), opts);
                  
            

            double dt;

            // Write to variables
            foreach (var sp in sol)
            {
                if (tsim.Count > 0)
                    dt = sp.T - tsim.Last();
                else
                    dt = sp.T;

                if (Double.IsNaN(sp.X[0]))
                    return;

                //xsim[plotLocs.Count].Add(dt);
                tsim.Add(sp.T);
                for (int i = 0; i < plotLocs.Count; i++)
                {
                    double x = 0.0;
                    foreach (int j in plotLocs[i])
                        x += sp.X[j];
                    xsim[i].Add(x);
                }
            }
        }

        private static Vector oscillating_ics()
        {
            // Assign values to x0
            int n = 187;
            var x0 = Vector.Zeros(n);
            x0[1] = 1.0;
            x0[2] = 100.0;
            x0[3] = 100.0;
            x0[4] = 100.0;
            x0[5] = 100.0;
            x0[6] = 100.0;
            x0[7] = 100.0;
            x0[8] = 100.0;
            x0[14] = 1.0;
            x0[15] = 100.0;
            x0[16] = 100.0;
            x0[17] = 100.0;
            x0[18] = 100.0;
            x0[19] = 100.0;
            x0[20] = 100.0;
            x0[21] = 100.0;
            x0[27] = 1.0;
            x0[28] = 100.0;
            x0[29] = 100.0;
            x0[30] = 100.0;
            x0[31] = 100.0;
            x0[32] = 100.0;
            x0[33] = 100.0;
            x0[34] = 100.0;
            x0[40] = 2.0;
            x0[44] = 2.0;
            x0[76] = 3.0;

            return (x0);
        }

        private static Vector derivs(double t, Vector x)
        {
            // Declare variables for concentrations
            double IGNORE, BJ2x2_23, BJ2x2_22, BJ2x2_21, BJ2x2_20, BJ2x2_19, BJ2x2_18, BJ2x2_17, BJ2x2_16, sp_3, sp_5, sp_4, sp_7, sp_6, BJ2x2_15, BJ2x2_14, BJ2x2_13, BJ2x2_12, BJ2x2_11, BJ2x2_10, BJ2x2_9, BJ2x2_8, sp_8, sp_10, sp_9, sp_12, sp_11, BJ2x2_7, BJ2x2_6, BJ2x2_5, BJ2x2_4, BJ2x2_3, BJ2x2_2, BJ2x2_1, BJ2x2, sp_13, sp_15, sp_14, sp_17, sp_16, sp_2, sp_18, sp_20, sp_19, sp_1, sp_21, sp_23, sp_24, sp_26, sp_29, sp_28, sp_31, sp_32, sp_30, sp_33, sp_34, sp_27, sp_36, sp_37, sp_35, sp_38, sp_39, sp_25, sp_40, sp_42, sp_43, sp_45, sp_44, sp_47, sp_46, sp_41, sp_49, sp_48, sp_22, sp_51, sp_50, sp_0, sp_55, sp_57, sp_58, sp_60, sp_63, sp_62, sp_65, sp_66, sp_69, sp_70, sp_71, sp_68, sp_72, sp_73, sp_67, sp_74, sp_75, sp_64, sp_76, sp_77, sp_61, sp_79, sp_80, sp_83, sp_84, sp_85, sp_82, sp_86, sp_87, sp_81, sp_88, sp_89, sp_78, sp_90, sp_91, sp_59, sp_92, sp_94, sp_95, sp_97, sp_96, sp_99, sp_98, sp_93, sp_100, sp_56, sp_102, sp_101, sp_54, sp_104, sp_107, sp_106, sp_111, sp_112, sp_115, sp_116, sp_117, sp_114, sp_118, sp_119, sp_113, sp_120, sp_121, sp_110, sp_122, sp_123, sp_105, sp_127, sp_128, sp_131, sp_132, sp_133, sp_130, sp_134, sp_135, sp_129, sp_136, sp_137, sp_126, sp_138, sp_139, sp_103, sp_140, sp_142, sp_143, sp_145, sp_146, sp_144, sp_148, sp_147, sp_141, sp_149, sp_125, sp_150, sp_151, sp_124, sp_152, sp_153, sp_109, sp_154, sp_155, sp_108, sp_156, sp_157, sp_53, sp_158, sp_159, sp_52, sp_160, sp_161;

            // Declare variables for derivatives
            double dBJ2x2_23, dBJ2x2_22, dBJ2x2_21, dBJ2x2_20, dBJ2x2_19, dBJ2x2_18, dBJ2x2_17, dBJ2x2_16, dsp_3, dsp_5, dsp_4, dsp_7, dsp_6, dBJ2x2_15, dBJ2x2_14, dBJ2x2_13, dBJ2x2_12, dBJ2x2_11, dBJ2x2_10, dBJ2x2_9, dBJ2x2_8, dsp_8, dsp_10, dsp_9, dsp_12, dsp_11, dBJ2x2_7, dBJ2x2_6, dBJ2x2_5, dBJ2x2_4, dBJ2x2_3, dBJ2x2_2, dBJ2x2_1, dBJ2x2, dsp_13, dsp_15, dsp_14, dsp_17, dsp_16, dsp_2, dsp_18, dsp_20, dsp_19, dsp_1, dsp_21, dsp_23, dsp_24, dsp_26, dsp_29, dsp_28, dsp_31, dsp_32, dsp_30, dsp_33, dsp_34, dsp_27, dsp_36, dsp_37, dsp_35, dsp_38, dsp_39, dsp_25, dsp_40, dsp_42, dsp_43, dsp_45, dsp_44, dsp_47, dsp_46, dsp_41, dsp_49, dsp_48, dsp_22, dsp_51, dsp_50, dsp_0, dsp_55, dsp_57, dsp_58, dsp_60, dsp_63, dsp_62, dsp_65, dsp_66, dsp_69, dsp_70, dsp_71, dsp_68, dsp_72, dsp_73, dsp_67, dsp_74, dsp_75, dsp_64, dsp_76, dsp_77, dsp_61, dsp_79, dsp_80, dsp_83, dsp_84, dsp_85, dsp_82, dsp_86, dsp_87, dsp_81, dsp_88, dsp_89, dsp_78, dsp_90, dsp_91, dsp_59, dsp_92, dsp_94, dsp_95, dsp_97, dsp_96, dsp_99, dsp_98, dsp_93, dsp_100, dsp_56, dsp_102, dsp_101, dsp_54, dsp_104, dsp_107, dsp_106, dsp_111, dsp_112, dsp_115, dsp_116, dsp_117, dsp_114, dsp_118, dsp_119, dsp_113, dsp_120, dsp_121, dsp_110, dsp_122, dsp_123, dsp_105, dsp_127, dsp_128, dsp_131, dsp_132, dsp_133, dsp_130, dsp_134, dsp_135, dsp_129, dsp_136, dsp_137, dsp_126, dsp_138, dsp_139, dsp_103, dsp_140, dsp_142, dsp_143, dsp_145, dsp_146, dsp_144, dsp_148, dsp_147, dsp_141, dsp_149, dsp_125, dsp_150, dsp_151, dsp_124, dsp_152, dsp_153, dsp_109, dsp_154, dsp_155, dsp_108, dsp_156, dsp_157, dsp_53, dsp_158, dsp_159, dsp_52, dsp_160, dsp_161;
            double[] dxdt;

            // Assign states
            IGNORE = x[0];
            BJ2x2_23 = x[1];
            BJ2x2_22 = x[2];
            BJ2x2_21 = x[3];
            BJ2x2_20 = x[4];
            BJ2x2_19 = x[5];
            BJ2x2_18 = x[6];
            BJ2x2_17 = x[7];
            BJ2x2_16 = x[8];
            sp_3 = x[9];
            sp_5 = x[10];
            sp_4 = x[11];
            sp_7 = x[12];
            sp_6 = x[13];
            BJ2x2_15 = x[14];
            BJ2x2_14 = x[15];
            BJ2x2_13 = x[16];
            BJ2x2_12 = x[17];
            BJ2x2_11 = x[18];
            BJ2x2_10 = x[19];
            BJ2x2_9 = x[20];
            BJ2x2_8 = x[21];
            sp_8 = x[22];
            sp_10 = x[23];
            sp_9 = x[24];
            sp_12 = x[25];
            sp_11 = x[26];
            BJ2x2_7 = x[27];
            BJ2x2_6 = x[28];
            BJ2x2_5 = x[29];
            BJ2x2_4 = x[30];
            BJ2x2_3 = x[31];
            BJ2x2_2 = x[32];
            BJ2x2_1 = x[33];
            BJ2x2 = x[34];
            sp_13 = x[35];
            sp_15 = x[36];
            sp_14 = x[37];
            sp_17 = x[38];
            sp_16 = x[39];
            sp_2 = x[40];
            sp_18 = x[41];
            sp_20 = x[42];
            sp_19 = x[43];
            sp_1 = x[44];
            sp_21 = x[45];
            sp_23 = x[46];
            sp_24 = x[47];
            sp_26 = x[48];
            sp_29 = x[49];
            sp_28 = x[50];
            sp_31 = x[51];
            sp_32 = x[52];
            sp_30 = x[53];
            sp_33 = x[54];
            sp_34 = x[55];
            sp_27 = x[56];
            sp_36 = x[57];
            sp_37 = x[58];
            sp_35 = x[59];
            sp_38 = x[60];
            sp_39 = x[61];
            sp_25 = x[62];
            sp_40 = x[63];
            sp_42 = x[64];
            sp_43 = x[65];
            sp_45 = x[66];
            sp_44 = x[67];
            sp_47 = x[68];
            sp_46 = x[69];
            sp_41 = x[70];
            sp_49 = x[71];
            sp_48 = x[72];
            sp_22 = x[73];
            sp_51 = x[74];
            sp_50 = x[75];
            sp_0 = x[76];
            sp_55 = x[77];
            sp_57 = x[78];
            sp_58 = x[79];
            sp_60 = x[80];
            sp_63 = x[81];
            sp_62 = x[82];
            sp_65 = x[83];
            sp_66 = x[84];
            sp_69 = x[85];
            sp_70 = x[86];
            sp_71 = x[87];
            sp_68 = x[88];
            sp_72 = x[89];
            sp_73 = x[90];
            sp_67 = x[91];
            sp_74 = x[92];
            sp_75 = x[93];
            sp_64 = x[94];
            sp_76 = x[95];
            sp_77 = x[96];
            sp_61 = x[97];
            sp_79 = x[98];
            sp_80 = x[99];
            sp_83 = x[100];
            sp_84 = x[101];
            sp_85 = x[102];
            sp_82 = x[103];
            sp_86 = x[104];
            sp_87 = x[105];
            sp_81 = x[106];
            sp_88 = x[107];
            sp_89 = x[108];
            sp_78 = x[109];
            sp_90 = x[110];
            sp_91 = x[111];
            sp_59 = x[112];
            sp_92 = x[113];
            sp_94 = x[114];
            sp_95 = x[115];
            sp_97 = x[116];
            sp_96 = x[117];
            sp_99 = x[118];
            sp_98 = x[119];
            sp_93 = x[120];
            sp_100 = x[121];
            sp_56 = x[122];
            sp_102 = x[123];
            sp_101 = x[124];
            sp_54 = x[125];
            sp_104 = x[126];
            sp_107 = x[127];
            sp_106 = x[128];
            sp_111 = x[129];
            sp_112 = x[130];
            sp_115 = x[131];
            sp_116 = x[132];
            sp_117 = x[133];
            sp_114 = x[134];
            sp_118 = x[135];
            sp_119 = x[136];
            sp_113 = x[137];
            sp_120 = x[138];
            sp_121 = x[139];
            sp_110 = x[140];
            sp_122 = x[141];
            sp_123 = x[142];
            sp_105 = x[143];
            sp_127 = x[144];
            sp_128 = x[145];
            sp_131 = x[146];
            sp_132 = x[147];
            sp_133 = x[148];
            sp_130 = x[149];
            sp_134 = x[150];
            sp_135 = x[151];
            sp_129 = x[152];
            sp_136 = x[153];
            sp_137 = x[154];
            sp_126 = x[155];
            sp_138 = x[156];
            sp_139 = x[157];
            sp_103 = x[158];
            sp_140 = x[159];
            sp_142 = x[160];
            sp_143 = x[161];
            sp_145 = x[162];
            sp_146 = x[163];
            sp_144 = x[164];
            sp_148 = x[165];
            sp_147 = x[166];
            sp_141 = x[167];
            sp_149 = x[168];
            sp_125 = x[169];
            sp_150 = x[170];
            sp_151 = x[171];
            sp_124 = x[172];
            sp_152 = x[173];
            sp_153 = x[174];
            sp_109 = x[175];
            sp_154 = x[176];
            sp_155 = x[177];
            sp_108 = x[178];
            sp_156 = x[179];
            sp_157 = x[180];
            sp_53 = x[181];
            sp_158 = x[182];
            sp_159 = x[183];
            sp_52 = x[184];
            sp_160 = x[185];
            sp_161 = x[186];

            // Write out the reaction propensities
            var r_0 = 0.0003 * BJ2x2_17 * sp_160;
            var r_1 = 0.0003 * sp_103 * sp_160;
            var r_2 = 0.1126 * sp_52;
            var r_3 = 0.1126 * sp_52;
            var r_4 = 0.0003 * BJ2x2_17 * sp_158;
            var r_5 = 0.0003 * sp_103 * sp_158;
            var r_6 = 0.1126 * sp_53;
            var r_7 = 0.1126 * sp_53;
            var r_8 = 0.0003 * BJ2x2_17 * sp_156;
            var r_9 = 0.0003 * sp_103 * sp_156;
            var r_10 = 0.1126 * sp_108;
            var r_11 = 0.1126 * sp_108;
            var r_12 = 0.0003 * BJ2x2_17 * sp_154;
            var r_13 = 0.0003 * sp_103 * sp_154;
            var r_14 = 0.1126 * sp_109;
            var r_15 = 0.1126 * sp_109;
            var r_16 = 0.0003 * BJ2x2_17 * sp_152;
            var r_17 = 0.0003 * sp_103 * sp_152;
            var r_18 = 0.1126 * sp_124;
            var r_19 = 0.1126 * sp_124;
            var r_20 = 0.0003 * BJ2x2_17 * sp_150;
            var r_21 = 0.0003 * sp_103 * sp_150;
            var r_22 = 0.1126 * sp_125;
            var r_23 = 0.1126 * sp_125;
            var r_24 = 0.0003 * sp_141 * BJ2x2_21;
            var r_25 = 0.0003 * sp_141 * sp_142;
            var r_26 = 0.0003 * sp_144 * BJ2x2_22;
            var r_27 = 0.0003 * sp_144 * sp_145;
            var r_28 = 0.0003 * sp_19 * sp_145;
            var r_29 = 0.1126 * sp_143;
            var r_30 = 0.1126 * sp_143;
            var r_31 = 0.0003 * BJ2x2_20 * sp_142;
            var r_32 = 0.1126 * sp_140;
            var r_33 = 0.1126 * sp_140;
            var r_34 = 0.0003 * sp_103 * BJ2x2_19;
            var r_35 = 0.0003 * sp_103 * sp_104;
            var r_36 = 0.0003 * sp_103 * sp_122;
            var r_37 = 0.0003 * sp_103 * sp_138;
            var r_38 = 0.0003 * BJ2x2_17 * sp_138;
            var r_39 = 0.1126 * sp_126;
            var r_40 = 0.1126 * sp_126;
            var r_41 = 0.0003 * BJ2x2_1 * sp_136;
            var r_42 = 0.0003 * sp_59 * sp_136;
            var r_43 = 0.1126 * sp_129;
            var r_44 = 0.1126 * sp_129;
            var r_45 = 0.0003 * BJ2x2_1 * sp_134;
            var r_46 = 0.0003 * sp_59 * sp_134;
            var r_47 = 0.1126 * sp_130;
            var r_48 = 0.1126 * sp_130;
            var r_49 = 0.0003 * BJ2x2_1 * sp_132;
            var r_50 = 0.0003 * sp_59 * sp_132;
            var r_51 = 0.1126 * sp_131;
            var r_52 = 0.1126 * sp_131;
            var r_53 = 0.0003 * sp_1 * sp_128;
            var r_54 = 0.0003 * sp_62 * sp_128;
            var r_55 = 0.0003 * sp_61 * sp_128;
            var r_56 = 0.0003 * sp_56 * sp_128;
            var r_57 = 0.1126 * sp_127;
            var r_58 = 0.1126 * sp_127;
            var r_59 = 0.0003 * sp_105 * sp_15;
            var r_60 = 0.0003 * sp_105 * sp_20;
            var r_61 = 0.0003 * sp_105 * sp_32;
            var r_62 = 0.0003 * sp_105 * sp_37;
            var r_63 = 0.0003 * BJ2x2_17 * sp_122;
            var r_64 = 0.1126 * sp_110;
            var r_65 = 0.1126 * sp_110;
            var r_66 = 0.0003 * BJ2x2_1 * sp_120;
            var r_67 = 0.0003 * sp_59 * sp_120;
            var r_68 = 0.1126 * sp_113;
            var r_69 = 0.1126 * sp_113;
            var r_70 = 0.0003 * BJ2x2_1 * sp_118;
            var r_71 = 0.0003 * sp_59 * sp_118;
            var r_72 = 0.1126 * sp_114;
            var r_73 = 0.1126 * sp_114;
            var r_74 = 0.0003 * BJ2x2_1 * sp_116;
            var r_75 = 0.0003 * sp_59 * sp_116;
            var r_76 = 0.1126 * sp_115;
            var r_77 = 0.1126 * sp_115;
            var r_78 = 0.0003 * sp_1 * sp_112;
            var r_79 = 0.0003 * sp_62 * sp_112;
            var r_80 = 0.0003 * sp_61 * sp_112;
            var r_81 = 0.0003 * sp_56 * sp_112;
            var r_82 = 0.1126 * sp_111;
            var r_83 = 0.1126 * sp_111;
            var r_84 = 0.0003 * sp_106 * sp_15;
            var r_85 = 0.0003 * sp_106 * sp_20;
            var r_86 = 0.0003 * sp_106 * sp_32;
            var r_87 = 0.0003 * sp_106 * sp_37;
            var r_88 = 0.0003 * BJ2x2_17 * sp_104;
            var r_89 = 0.1126 * sp_54;
            var r_90 = 0.1126 * sp_54;
            var r_91 = 0.0003 * sp_56 * sp_57;
            var r_92 = 0.0003 * sp_56 * sp_97;
            var r_93 = 0.0003 * sp_93 * BJ2x2_5;
            var r_94 = 0.0003 * sp_93 * sp_94;
            var r_95 = 0.0003 * sp_96 * BJ2x2_6;
            var r_96 = 0.0003 * sp_96 * sp_97;
            var r_97 = 0.1126 * sp_95;
            var r_98 = 0.1126 * sp_95;
            var r_99 = 0.0003 * BJ2x2_4 * sp_94;
            var r_100 = 0.1126 * sp_92;
            var r_101 = 0.1126 * sp_92;
            var r_102 = 0.0003 * sp_59 * BJ2x2_3;
            var r_103 = 0.0003 * sp_59 * sp_60;
            var r_104 = 0.0003 * sp_59 * sp_76;
            var r_105 = 0.0003 * sp_59 * sp_90;
            var r_106 = 0.0003 * BJ2x2_1 * sp_90;
            var r_107 = 0.1126 * sp_78;
            var r_108 = 0.1126 * sp_78;
            var r_109 = 0.0003 * BJ2x2_9 * sp_88;
            var r_110 = 0.0003 * sp_25 * sp_88;
            var r_111 = 0.1126 * sp_81;
            var r_112 = 0.1126 * sp_81;
            var r_113 = 0.0003 * BJ2x2_9 * sp_86;
            var r_114 = 0.0003 * sp_25 * sp_86;
            var r_115 = 0.1126 * sp_82;
            var r_116 = 0.1126 * sp_82;
            var r_117 = 0.0003 * BJ2x2_9 * sp_84;
            var r_118 = 0.0003 * sp_25 * sp_84;
            var r_119 = 0.1126 * sp_83;
            var r_120 = 0.1126 * sp_83;
            var r_121 = 0.0003 * sp_2 * sp_80;
            var r_122 = 0.0003 * sp_28 * sp_80;
            var r_123 = 0.0003 * sp_27 * sp_80;
            var r_124 = 0.0003 * sp_22 * sp_80;
            var r_125 = 0.1126 * sp_79;
            var r_126 = 0.1126 * sp_79;
            var r_127 = 0.0003 * sp_61 * sp_10;
            var r_128 = 0.0003 * sp_61 * sp_57;
            var r_129 = 0.0003 * BJ2x2_1 * sp_76;
            var r_130 = 0.1126 * sp_64;
            var r_131 = 0.1126 * sp_64;
            var r_132 = 0.0003 * BJ2x2_9 * sp_74;
            var r_133 = 0.0003 * sp_25 * sp_74;
            var r_134 = 0.1126 * sp_67;
            var r_135 = 0.1126 * sp_67;
            var r_136 = 0.0003 * BJ2x2_9 * sp_72;
            var r_137 = 0.0003 * sp_25 * sp_72;
            var r_138 = 0.1126 * sp_68;
            var r_139 = 0.1126 * sp_68;
            var r_140 = 0.0003 * BJ2x2_9 * sp_70;
            var r_141 = 0.0003 * sp_25 * sp_70;
            var r_142 = 0.1126 * sp_69;
            var r_143 = 0.1126 * sp_69;
            var r_144 = 0.0003 * sp_2 * sp_66;
            var r_145 = 0.0003 * sp_28 * sp_66;
            var r_146 = 0.0003 * sp_27 * sp_66;
            var r_147 = 0.0003 * sp_22 * sp_66;
            var r_148 = 0.1126 * sp_65;
            var r_149 = 0.1126 * sp_65;
            var r_150 = 0.0003 * sp_62 * sp_10;
            var r_151 = 0.0003 * sp_62 * sp_57;
            var r_152 = 0.0003 * BJ2x2_1 * sp_60;
            var r_153 = 0.1126 * sp_58;
            var r_154 = 0.1126 * sp_58;
            var r_155 = 0.0003 * sp_1 * sp_57;
            var r_156 = 0.1126 * sp_55;
            var r_157 = 0.1126 * sp_55;
            var r_158 = 0.0003 * sp_0 * sp_15;
            var r_159 = 0.0003 * sp_0 * sp_20;
            var r_160 = 0.0003 * sp_0 * sp_32;
            var r_161 = 0.0003 * sp_0 * sp_37;
            var r_162 = 0.0003 * sp_22 * sp_23;
            var r_163 = 0.0003 * sp_22 * sp_45;
            var r_164 = 0.0003 * sp_41 * BJ2x2_13;
            var r_165 = 0.0003 * sp_41 * sp_42;
            var r_166 = 0.0003 * sp_44 * BJ2x2_14;
            var r_167 = 0.0003 * sp_44 * sp_45;
            var r_168 = 0.1126 * sp_43;
            var r_169 = 0.1126 * sp_43;
            var r_170 = 0.0003 * BJ2x2_12 * sp_42;
            var r_171 = 0.1126 * sp_40;
            var r_172 = 0.1126 * sp_40;
            var r_173 = 0.0003 * sp_25 * BJ2x2_11;
            var r_174 = 0.0003 * sp_25 * sp_26;
            var r_175 = 0.0003 * sp_25 * sp_33;
            var r_176 = 0.0003 * sp_25 * sp_38;
            var r_177 = 0.0003 * BJ2x2_9 * sp_38;
            var r_178 = 0.1126 * sp_35;
            var r_179 = 0.1126 * sp_35;
            var r_180 = 0.0003 * sp_19 * sp_37;
            var r_181 = 0.1126 * sp_36;
            var r_182 = 0.1126 * sp_36;
            var r_183 = 0.0003 * sp_27 * sp_5;
            var r_184 = 0.0003 * sp_27 * sp_23;
            var r_185 = 0.0003 * BJ2x2_9 * sp_33;
            var r_186 = 0.1126 * sp_30;
            var r_187 = 0.1126 * sp_30;
            var r_188 = 0.0003 * sp_19 * sp_32;
            var r_189 = 0.1126 * sp_31;
            var r_190 = 0.1126 * sp_31;
            var r_191 = 0.0003 * sp_28 * sp_5;
            var r_192 = 0.0003 * sp_28 * sp_23;
            var r_193 = 0.0003 * BJ2x2_9 * sp_26;
            var r_194 = 0.1126 * sp_24;
            var r_195 = 0.1126 * sp_24;
            var r_196 = 0.0003 * sp_2 * sp_23;
            var r_197 = 0.1126 * sp_21;
            var r_198 = 0.1126 * sp_21;
            var r_199 = 0.0003 * sp_1 * sp_10;
            var r_200 = 0.0003 * sp_19 * sp_20;
            var r_201 = 0.1126 * sp_18;
            var r_202 = 0.1126 * sp_18;
            var r_203 = 0.0003 * sp_2 * sp_5;
            var r_204 = 0.0003 * sp_14 * BJ2x2_2;
            var r_205 = 0.0003 * sp_14 * sp_15;
            var r_206 = 0.1126 * sp_13;
            var r_207 = 0.1126 * sp_13;
            var r_208 = 0.0003 * BJ2x2_7 * BJ2x2;
            var r_209 = 0.0003 * sp_9 * BJ2x2_10;
            var r_210 = 0.0003 * sp_9 * sp_10;
            var r_211 = 0.1126 * sp_8;
            var r_212 = 0.1126 * sp_8;
            var r_213 = 0.0003 * BJ2x2_15 * BJ2x2_8;
            var r_214 = 0.0003 * sp_4 * BJ2x2_18;
            var r_215 = 0.0003 * sp_4 * sp_5;
            var r_216 = 0.1126 * sp_3;
            var r_217 = 0.1126 * sp_3;
            var r_218 = 0.0003 * BJ2x2_23 * BJ2x2_16;

            // Assign derivatives
            dBJ2x2_23 = r_0 + r_4 + r_8 + r_12 + r_16 + r_20 + r_38 + r_63 + r_88 + r_216 - r_218;
            dBJ2x2_22 = -r_26;
            dBJ2x2_21 = -r_24;
            dBJ2x2_20 = r_30 - r_31;
            dBJ2x2_19 = r_33 - r_34;
            dBJ2x2_18 = -r_214;
            dBJ2x2_17 = -r_0 - r_4 - r_8 - r_12 - r_16 - r_20 - r_38 - r_63 - r_88;
            dBJ2x2_16 = r_216 - r_218;
            dsp_3 = r_215 - r_216 - r_217 + r_218;
            dsp_5 = r_181 - r_183 + r_189 - r_191 + r_201 - r_203 - r_215 + r_217;
            dsp_4 = -r_214 - r_215 + r_217;
            dsp_7 = r_214;
            dsp_6 = r_214;
            dBJ2x2_15 = r_109 + r_113 + r_117 + r_132 + r_136 + r_140 + r_177 + r_185 + r_193 + r_211 - r_213;
            dBJ2x2_14 = -r_166;
            dBJ2x2_13 = -r_164;
            dBJ2x2_12 = r_169 - r_170;
            dBJ2x2_11 = r_172 - r_173;
            dBJ2x2_10 = -r_209;
            dBJ2x2_9 = -r_109 - r_113 - r_117 - r_132 - r_136 - r_140 - r_177 - r_185 - r_193;
            dBJ2x2_8 = r_211 - r_213;
            dsp_8 = r_210 - r_211 - r_212 + r_213;
            dsp_10 = r_125 - r_127 + r_148 - r_150 + r_197 - r_199 - r_210 + r_212;
            dsp_9 = -r_209 - r_210 + r_212;
            dsp_12 = r_209;
            dsp_11 = r_209;
            dBJ2x2_7 = r_41 + r_45 + r_49 + r_66 + r_70 + r_74 + r_106 + r_129 + r_152 + r_206 - r_208;
            dBJ2x2_6 = -r_95;
            dBJ2x2_5 = -r_93;
            dBJ2x2_4 = r_98 - r_99;
            dBJ2x2_3 = r_101 - r_102;
            dBJ2x2_2 = -r_204;
            dBJ2x2_1 = -r_41 - r_45 - r_49 - r_66 - r_70 - r_74 - r_106 - r_129 - r_152;
            dBJ2x2 = r_206 - r_208;
            dsp_13 = r_205 - r_206 - r_207 + r_208;
            dsp_15 = r_57 - r_59 + r_82 - r_84 + r_156 - r_158 - r_205 + r_207;
            dsp_14 = -r_204 - r_205 + r_207;
            dsp_17 = r_204;
            dsp_16 = r_204;
            dsp_2 = r_119 - r_121 + r_142 - r_144 + r_194 - r_196 + r_201 - r_203;
            dsp_18 = r_200 - r_201 - r_202 + r_203;
            dsp_20 = r_39 - r_60 + r_64 - r_85 + r_89 - r_159 - r_200 + r_202;
            dsp_19 = -r_28 - r_180 + r_182 - r_188 + r_190 - r_200 + r_202;
            dsp_1 = r_51 - r_53 + r_76 - r_78 + r_153 - r_155 + r_197 - r_199;
            dsp_21 = r_162 - r_197 - r_198 + r_199;
            dsp_23 = -r_162 + r_178 - r_184 + r_186 - r_192 + r_194 - r_196 + r_198;
            dsp_24 = r_174 - r_194 - r_195 + r_196;
            dsp_26 = -r_174 - r_193 + r_195;
            dsp_29 = r_193;
            dsp_28 = r_109 + r_113 + r_115 + r_117 - r_122 + r_132 + r_136 + r_138 + r_140 - r_145 + r_177 + r_185 + r_186 + r_189 - r_191 - r_192 + r_193;
            dsp_31 = r_188 - r_189 - r_190 + r_191;
            dsp_32 = r_6 + r_14 + r_22 - r_61 - r_86 - r_160 - r_188 + r_190;
            dsp_30 = r_175 - r_186 - r_187 + r_192;
            dsp_33 = -r_175 - r_185 + r_187;
            dsp_34 = r_185;
            dsp_27 = r_109 + r_111 + r_113 + r_117 - r_123 + r_132 + r_134 + r_136 + r_140 - r_146 + r_177 + r_178 + r_181 - r_183 - r_184 + r_185 + r_193;
            dsp_36 = r_180 - r_181 - r_182 + r_183;
            dsp_37 = r_2 + r_10 + r_18 - r_62 - r_87 - r_161 - r_180 + r_182;
            dsp_35 = r_176 - r_178 - r_179 + r_184;
            dsp_38 = -r_176 - r_177 + r_179;
            dsp_39 = r_177;
            dsp_25 = -r_110 + r_112 - r_114 + r_116 - r_118 + r_120 - r_133 + r_135 - r_137 + r_139 - r_141 + r_143 + r_172 - r_173 - r_174 - r_175 - r_176 + r_179 + r_187 + r_195;
            dsp_40 = r_165 - r_171 - r_172 + r_173;
            dsp_42 = -r_165 + r_169 - r_170 + r_171;
            dsp_43 = r_167 - r_168 - r_169 + r_170;
            dsp_45 = -r_163 - r_167 + r_168;
            dsp_44 = -r_166 - r_167 + r_168;
            dsp_47 = r_166;
            dsp_46 = r_166;
            dsp_41 = -r_164 - r_165 + r_171;
            dsp_49 = r_164;
            dsp_48 = r_28 + r_164;
            dsp_22 = -r_124 + r_126 - r_147 + r_149 - r_162 - r_163 + r_198;
            dsp_51 = r_163;
            dsp_50 = r_93 + r_163;
            dsp_0 = r_2 + r_6 + r_89 + r_156 - r_158 - r_159 - r_160 - r_161;
            dsp_55 = r_91 - r_156 - r_157 + r_158;
            dsp_57 = -r_91 + r_107 - r_128 + r_130 - r_151 + r_153 - r_155 + r_157;
            dsp_58 = r_103 - r_153 - r_154 + r_155;
            dsp_60 = -r_103 - r_152 + r_154;
            dsp_63 = r_152;
            dsp_62 = r_41 + r_45 + r_47 + r_49 - r_54 + r_66 + r_70 + r_72 + r_74 - r_79 + r_106 + r_129 + r_130 + r_148 - r_150 - r_151 + r_152;
            dsp_65 = r_147 - r_148 - r_149 + r_150;
            dsp_66 = r_134 + r_138 + r_142 - r_144 - r_145 - r_146 - r_147 + r_149;
            dsp_69 = r_141 - r_142 - r_143 + r_144;
            dsp_70 = -r_140 - r_141 + r_143;
            dsp_71 = r_140;
            dsp_68 = r_137 - r_138 - r_139 + r_145;
            dsp_72 = -r_136 - r_137 + r_139;
            dsp_73 = r_136;
            dsp_67 = r_133 - r_134 - r_135 + r_146;
            dsp_74 = -r_132 - r_133 + r_135;
            dsp_75 = r_132;
            dsp_64 = r_104 - r_130 - r_131 + r_151;
            dsp_76 = -r_104 - r_129 + r_131;
            dsp_77 = r_129;
            dsp_61 = r_41 + r_43 + r_45 + r_49 - r_55 + r_66 + r_68 + r_70 + r_74 - r_80 + r_106 + r_107 + r_125 - r_127 - r_128 + r_129 + r_152;
            dsp_79 = r_124 - r_125 - r_126 + r_127;
            dsp_80 = r_111 + r_115 + r_119 - r_121 - r_122 - r_123 - r_124 + r_126;
            dsp_83 = r_118 - r_119 - r_120 + r_121;
            dsp_84 = -r_117 - r_118 + r_120;
            dsp_85 = r_117;
            dsp_82 = r_114 - r_115 - r_116 + r_122;
            dsp_86 = -r_113 - r_114 + r_116;
            dsp_87 = r_113;
            dsp_81 = r_110 - r_111 - r_112 + r_123;
            dsp_88 = -r_109 - r_110 + r_112;
            dsp_89 = r_109;
            dsp_78 = r_105 - r_107 - r_108 + r_128;
            dsp_90 = -r_105 - r_106 + r_108;
            dsp_91 = r_106;
            dsp_59 = -r_42 + r_44 - r_46 + r_48 - r_50 + r_52 - r_67 + r_69 - r_71 + r_73 - r_75 + r_77 + r_101 - r_102 - r_103 - r_104 - r_105 + r_108 + r_131 + r_154;
            dsp_92 = r_94 - r_100 - r_101 + r_102;
            dsp_94 = -r_94 + r_98 - r_99 + r_100;
            dsp_95 = r_96 - r_97 - r_98 + r_99;
            dsp_97 = -r_92 - r_96 + r_97;
            dsp_96 = -r_95 - r_96 + r_97;
            dsp_99 = r_95;
            dsp_98 = r_95;
            dsp_93 = -r_93 - r_94 + r_100;
            dsp_100 = r_93;
            dsp_56 = -r_56 + r_58 - r_81 + r_83 - r_91 - r_92 + r_157;
            dsp_102 = r_92;
            dsp_101 = r_24 + r_92;
            dsp_54 = r_35 - r_89 - r_90 + r_159;
            dsp_104 = -r_35 - r_88 + r_90;
            dsp_107 = r_88;
            dsp_106 = r_0 + r_4 + r_8 + r_10 + r_12 + r_14 + r_16 + r_20 + r_38 + r_63 + r_64 + r_82 - r_84 - r_85 - r_86 - r_87 + r_88;
            dsp_111 = r_81 - r_82 - r_83 + r_84;
            dsp_112 = r_68 + r_72 + r_76 - r_78 - r_79 - r_80 - r_81 + r_83;
            dsp_115 = r_75 - r_76 - r_77 + r_78;
            dsp_116 = -r_74 - r_75 + r_77;
            dsp_117 = r_74;
            dsp_114 = r_71 - r_72 - r_73 + r_79;
            dsp_118 = -r_70 - r_71 + r_73;
            dsp_119 = r_70;
            dsp_113 = r_67 - r_68 - r_69 + r_80;
            dsp_120 = -r_66 - r_67 + r_69;
            dsp_121 = r_66;
            dsp_110 = r_36 - r_64 - r_65 + r_85;
            dsp_122 = -r_36 - r_63 + r_65;
            dsp_123 = r_63;
            dsp_105 = r_0 + r_4 + r_8 + r_12 + r_16 + r_18 + r_20 + r_22 + r_38 + r_39 + r_57 - r_59 - r_60 - r_61 - r_62 + r_63 + r_88;
            dsp_127 = r_56 - r_57 - r_58 + r_59;
            dsp_128 = r_43 + r_47 + r_51 - r_53 - r_54 - r_55 - r_56 + r_58;
            dsp_131 = r_50 - r_51 - r_52 + r_53;
            dsp_132 = -r_49 - r_50 + r_52;
            dsp_133 = r_49;
            dsp_130 = r_46 - r_47 - r_48 + r_54;
            dsp_134 = -r_45 - r_46 + r_48;
            dsp_135 = r_45;
            dsp_129 = r_42 - r_43 - r_44 + r_55;
            dsp_136 = -r_41 - r_42 + r_44;
            dsp_137 = r_41;
            dsp_126 = r_37 - r_39 - r_40 + r_60;
            dsp_138 = -r_37 - r_38 + r_40;
            dsp_139 = r_38;
            dsp_103 = -r_1 + r_3 - r_5 + r_7 - r_9 + r_11 - r_13 + r_15 - r_17 + r_19 - r_21 + r_23 + r_33 - r_34 - r_35 - r_36 - r_37 + r_40 + r_65 + r_90;
            dsp_140 = r_25 - r_32 - r_33 + r_34;
            dsp_142 = -r_25 + r_30 - r_31 + r_32;
            dsp_143 = r_27 - r_29 - r_30 + r_31;
            dsp_145 = -r_27 - r_28 + r_29;
            dsp_146 = r_28;
            dsp_144 = -r_26 - r_27 + r_29;
            dsp_148 = r_26;
            dsp_147 = r_26;
            dsp_141 = -r_24 - r_25 + r_32;
            dsp_149 = r_24;
            dsp_125 = r_21 - r_22 - r_23 + r_61;
            dsp_150 = -r_20 - r_21 + r_23;
            dsp_151 = r_20;
            dsp_124 = r_17 - r_18 - r_19 + r_62;
            dsp_152 = -r_16 - r_17 + r_19;
            dsp_153 = r_16;
            dsp_109 = r_13 - r_14 - r_15 + r_86;
            dsp_154 = -r_12 - r_13 + r_15;
            dsp_155 = r_12;
            dsp_108 = r_9 - r_10 - r_11 + r_87;
            dsp_156 = -r_8 - r_9 + r_11;
            dsp_157 = r_8;
            dsp_53 = r_5 - r_6 - r_7 + r_160;
            dsp_158 = -r_4 - r_5 + r_7;
            dsp_159 = r_4;
            dsp_52 = r_1 - r_2 - r_3 + r_161;
            dsp_160 = -r_0 - r_1 + r_3;
            dsp_161 = r_0;
            dxdt = new double[] { 0.0, dBJ2x2_23, dBJ2x2_22, dBJ2x2_21, dBJ2x2_20, dBJ2x2_19, dBJ2x2_18, dBJ2x2_17, dBJ2x2_16, dsp_3, dsp_5, dsp_4, dsp_7, dsp_6, dBJ2x2_15, dBJ2x2_14, dBJ2x2_13, dBJ2x2_12, dBJ2x2_11, dBJ2x2_10, dBJ2x2_9, dBJ2x2_8, dsp_8, dsp_10, dsp_9, dsp_12, dsp_11, dBJ2x2_7, dBJ2x2_6, dBJ2x2_5, dBJ2x2_4, dBJ2x2_3, dBJ2x2_2, dBJ2x2_1, dBJ2x2, dsp_13, dsp_15, dsp_14, dsp_17, dsp_16, dsp_2, dsp_18, dsp_20, dsp_19, dsp_1, dsp_21, dsp_23, dsp_24, dsp_26, dsp_29, dsp_28, dsp_31, dsp_32, dsp_30, dsp_33, dsp_34, dsp_27, dsp_36, dsp_37, dsp_35, dsp_38, dsp_39, dsp_25, dsp_40, dsp_42, dsp_43, dsp_45, dsp_44, dsp_47, dsp_46, dsp_41, dsp_49, dsp_48, dsp_22, dsp_51, dsp_50, dsp_0, dsp_55, dsp_57, dsp_58, dsp_60, dsp_63, dsp_62, dsp_65, dsp_66, dsp_69, dsp_70, dsp_71, dsp_68, dsp_72, dsp_73, dsp_67, dsp_74, dsp_75, dsp_64, dsp_76, dsp_77, dsp_61, dsp_79, dsp_80, dsp_83, dsp_84, dsp_85, dsp_82, dsp_86, dsp_87, dsp_81, dsp_88, dsp_89, dsp_78, dsp_90, dsp_91, dsp_59, dsp_92, dsp_94, dsp_95, dsp_97, dsp_96, dsp_99, dsp_98, dsp_93, dsp_100, dsp_56, dsp_102, dsp_101, dsp_54, dsp_104, dsp_107, dsp_106, dsp_111, dsp_112, dsp_115, dsp_116, dsp_117, dsp_114, dsp_118, dsp_119, dsp_113, dsp_120, dsp_121, dsp_110, dsp_122, dsp_123, dsp_105, dsp_127, dsp_128, dsp_131, dsp_132, dsp_133, dsp_130, dsp_134, dsp_135, dsp_129, dsp_136, dsp_137, dsp_126, dsp_138, dsp_139, dsp_103, dsp_140, dsp_142, dsp_143, dsp_145, dsp_146, dsp_144, dsp_148, dsp_147, dsp_141, dsp_149, dsp_125, dsp_150, dsp_151, dsp_124, dsp_152, dsp_153, dsp_109, dsp_154, dsp_155, dsp_108, dsp_156, dsp_157, dsp_53, dsp_158, dsp_159, dsp_52, dsp_160, dsp_161 };

            return (new Vector(dxdt));
        }
        
        public void prepare() { }
    }
}