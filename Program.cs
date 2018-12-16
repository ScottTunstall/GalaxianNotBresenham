using System;

namespace NotBresenham
{



    //1172: DD 66 19      ld h,(ix +$19)          ; read INFLIGHT_ALIEN.DistanceFromOrigin
    //1175: DD 6E 1A ld   l, (ix+$1a)
    //1178: DD 56 1B ld   d,(ix+$1b)
    //117B: DD 5E 1C ld   e,(ix+$1c)

    //117E: 7D            ld a, l
    //117F: 4C ld   c,h
    //1180: 87            add a, a
    //1181: 30 01         jr nc,$1184

    //1183: 25            dec h
    //1184: 82            add a, d
    //1185: 57            ld d, a
    //1186: 3E 00         ld a,$00
    //1188: 8C adc  a,h

    //1189: FE 80         cp   $80
    //118B: 20 01         jr nz,$118E

    //118D: 79            ld a, c

    //118E: 67            ld h, a
    //118F: 4D            ld c, l
    //1190: ED 44         neg
    //1192: 87            add a, a
    //1193: 30 01         jr nc,$1196

    //1195: 2D            dec l

    //1196: 83            add a, e
    //1197: 5F            ld e, a
    //1198: 3E 00         ld a,$00
    //119A: 8D            adc a, l
    //119B: FE 80         cp   $80
    //119D: 20 01         jr nz,$11A0

    //119F: 79            ld a, c

    //11A0: 6F            ld l, a

    //11A1: 10 DB djnz $117E

    //11A3: DD 74 19      ld(ix+$19),h
    //11A6: DD 75 1A ld(ix+$1a),l
    //11A9: DD 72 1B ld(ix+$1b),d
    //11AC: DD 73 1C ld(ix+$1c),e
    //11AF: C9 ret




    class Program
    {
        private static bool CF { get; set; }

        private static byte A;
        private static byte C;
        private static byte H;
        private static byte L;
        private static byte D;
        private static byte E;


        static void Main(string[] args)
        {
            H = 0x42;
            L = 0;
            D = 0;
            E = 0;
                  
            for (byte B = 0; B < 255; B++)
            {
                Console.WriteLine("H:" + H.ToString("X"));
                Console.WriteLine("L:" + L.ToString("X"));
                Console.WriteLine("D:" + D.ToString("X"));
                Console.WriteLine("E:" + E.ToString("X"));

                Console.WriteLine();

                DoSomething();

                
            }

            Console.WriteLine("RESULTS");
            Console.WriteLine("=======");
            Console.WriteLine("H:" + H.ToString("X"));
            Console.WriteLine("L:" + L.ToString("X"));
            Console.WriteLine("D:" + D.ToString("X"));
            Console.WriteLine("E:" + E.ToString("X"));

            Console.WriteLine();

            Console.ReadKey();
        }

        static void DoSomething()
        {
            A = L;                                // ld   a,l

            C = H;                                // ld   c,h

            A = Add8(A, A);                       // add  a,a 

            if (CF)                               // jr nc,$1184
            {
                H--;                              // dec  h
            }

            A = Add8(A, D);                       // add  a,d
            D = A;                                // ld   d,a

            A = 0;                                // ld   a,$00
            A = Adc8(A, H);                       // adc a, h
                
            if (A == 0x80)                        // cp   $80 
            {                                    
                A = C;                            // ld a, c
            }

            H = A;                                // ld   h,a



            C = L;                                // ld   c,l
                                                  
            A = (byte)(256 - A);                  // neg
            A = Add8(A, A);                       // add  a,a
            if (CF)
            {
                L--;                              // dec  l
            }

            A = Add8(A, E);                      // add  a, e
            E = A;                               // ld   e,a  

            A = 0;                               // ld   a,$00
            A = Adc8(A, L);                      // adc  a,l

            if (A == 0x80)                       // cp   $80
            {
                A = C;                           // ld   a, c
            }

            L = A;                               // ld   l,a
        }


        private static byte Adc8(byte first, byte second)
        {
            int temp = first + second + (CF? (byte)1 : (byte)0);
            CF = (temp > 0xff);
            return (byte)(temp & 0xff);
        }


        static byte Add8(byte first, byte second)
        {
            int temp = first + second;
            CF = (temp > 0xff);
            return (byte) (temp & 0xff);
        }

        
    }
}
