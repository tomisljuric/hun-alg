using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madjarski_algoritam
{
    class Madjarski
    {
        public int n;
        public int[][] cost = new int[31][]; //matrica troškova
        public int max_match; //n radnika i n poslova
        public int[] lx = new int[31];
        public int[] ly = new int[31]; //oznake od X i Y dijelova
        public int[] xy = new int[31]; //xy[x] - vrh koji se podudara s x
        public int[] yx = new int[31]; //yx[y] - vrh koji se podudara s y
        public int[] S = new int[31]; //postavljanje S u algoritam
        public int[] T = new int[31]; //postavljanje T u algoritam
        public int[] slack = new int[31];
        public int[] slackx = new int[31]; //slackx[y] - vrh koji se podudara s y
        public int[] previous = new int[31]; //niz za memomiranje izmjeničnih p

        public void Inicijalizacija() 
        {
            for (int i = 0; i < lx.Length; i++) lx[i] = 0;
            for (int i = 0; i < ly.Length; i++) ly[i] = 0;
            for (int x = 0; x < n; x++)
                for (int y = 0; y < n; y++)
                    lx[x] = Math.Max(lx[x], cost[x][y]);
        }


        public void Azuriranje()
        {
            int x, y;
            int delta = 99999999; //inicijaliziranje delte kao beskonačnost
            for (y = 0; y < n; y++) //računjanje delte koristeći slack
                if (T[y] == 0)
                    delta = Math.Min(delta, slack[y]);
            for (x = 0; x < n; x++) //ažuriranje X
                if (S[x] == 1)
                    lx[x] -= delta;
            for (y = 0; y < n; y++) //ažuriranje Y
                if (T[y] == 1)
                    ly[y] += delta;
            for (y = 0; y < n; y++) //ažuriranje slack niza
                if (T[y] == 0)
                    slack[y] -= delta;
        }

        public void Dodaj_u_stablo(int x, int previous_x) //x - trenutni vrh, previous_x - vrh od x prije x u izmjeničnoj putanji
        {
            S[x] = 1; //dodavanje x u S
            previous[x] = previous_x; //ovo nam treba kod povećanja
            for (int y = 0; y < n; y++) //ažurirati slack jer dodajemo novi vrh na S
                if (lx[x] + ly[y] - cost[x][y] < slack[y])
                {
                    slack[y] = lx[x] + ly[y] - cost[x][y];
                    slackx[y] = x;
                }
        }



        public void Povecanje() //glavna funkcija algoritma
        {
            if (max_match == n) return; //provjera jel podudaranje već savršeno
            int x, y; //samo brojači i korijenski vrh
            int[] q = new int[n]; //q - red za bfs
            int wr = 0, rd = 0;  //wr,rd - pisanje i čitanje

            for (int i = 0; i < S.Length; i++) S[i] = 0;
            for (int i = 0; i < T.Length; i++) T[i] = 0;
            for (int i = 0; i < previous.Length; i++) previous[i] = -1;
            int root = -1;

            for (x = 0; x < n; x++) //pronalazak korijena stabla
            {
                if (xy[x] == -1)
                {
                    q[wr] = root = x;
                    wr++;
                    previous[x] = -2;
                    S[x] = 1;
                    break;
                }
            }
            if (root == -1)
            {
                // Svi su vrhovi već usklađeni
                return;
            }
            for (y = 0; y < n; y++) //inicijaliziranje slack niza
            {
                slack[y] = lx[root] + ly[y] - cost[root][y];
                slackx[y] = root;
            }

            //drugi dio funkcije
            while (true) //glavni ciklus
            {
                while (rd < wr) //radeći stablo sa bfs ciklusom
                {
                    x = q[rd++]; //trenutni vrh iz X dijela
                    for (y = 0; y < n; y++) //iterirati kroz sve rubove u grafu jednakosti
                        if (cost[x][y] == lx[x] + ly[y] && T[y] == 0)
                        {
                            if (yx[y] == -1) break; //nađen izloženi vrh u Y , dopunski put postoji
                            T[y] = 1; //inače samo dodati y na T
                            q[wr] = yx[y]; //dodati vrh yx[y] koji je usklađen
                            wr++;
                            Dodaj_u_stablo(yx[y], x); //dodati rubove (x,y) i (y,yx[y]) stablu
                        }
                    if (y < n)
                        break; //pronađen put za povećanje
                }
                if (y < n)
                    break; //pronađen put za povećanje

                Azuriranje(); //put za povećanje nije pronađen stoga poboljšajte označavanje

                wr = rd = 0;
                for (y = 0; y < n; y++)
                    //u ovom ciklusu dodajemo rubove koji su dodani grafu jednakosti kao
                    //rezultat poboljšanja označavanja, dodajemo rub (slackx[y],y) stablu ako i samo
                    //!T[y] && slack[y]==0, također s ovim rubom dodajemo još jedan 
                    //(y, yx[y]) ili povećamo podudaranje, ako je y bio izložen.
                    if (T[y] == 0 && slack[y] == 0)
                    {
                        if (yx[y] == -1) //izloženi vrh u Y pronađen - postoji staza za povećanje
                        {
                            x = slackx[y];
                            break;
                        }
                        else
                        {
                            T[y] = 1; //inače samo dodati y na T
                            if (S[yx[y]] == 0)
                            {
                                q[wr++] = yx[y]; //dodati vrh yx[y], koji se podudara s y, u red
                                Dodaj_u_stablo(yx[y], slackx[y]); //i dodajte rubove (x,y) i (y, yx[y]) stablu
                            }
                        }
                    }
                if (y < n) break; //pronađen put za povećanje
            }

            if (y < n) //pronašli smo put za povećanje
            {
                max_match++; //povećaj podudaranje, u ovom ciklusu inverziramo rubove duž povećavajuće staze
                for (int cx = x, cy = y, ty; cx != -2; cx = previous[cx], cy = ty)
                {
                    ty = xy[cx];
                    yx[cy] = cx;
                    xy[cx] = cy;
                }
                Povecanje(); //opozivi funkciju
            }
        }

        public int Madariziraj()
        {
            int ret = 0; //težina optimalnog podudaranja
            max_match = 0; //broj vrhova u trenutnom podudaranju
            xy = new int[n];
            yx = new int[n];
            for (int i = 0; i < xy.Length; i++) xy[i] = -1;
            for (int i = 0; i < yx.Length; i++) yx[i] = -1;
            Inicijalizacija(); 
            Povecanje(); 

            for (int x = 0; x < n; x++) //formiranje odgovora
                ret += cost[x][xy[x]];

            return ret;
        }

        public int Dodjela()
        {

            Console.WriteLine("Unesite matricu troškova tako što ćete vrijednosti odvojiti razmakom, red po red:");
            for (int i = 0; i < n; i++)
            {
                string[] v = Console.ReadLine().Split(' ');
                for (int j = 0; j < n; j++)
                {
                    cost[i][j] = -1 * int.Parse(v[j]); //negiranjem vrijednosti možemo pronaći maksimalnu težinu koja se podudara u modificiranom problemu.
                }
            }
            int rez = -1 * Madariziraj();
            return rez;
        }
    }
}
