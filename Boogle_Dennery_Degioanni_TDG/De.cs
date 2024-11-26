using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boogle_Dennery_Degioanni_TDG
{
    internal class De
    {
        private char[] faces;
        private char face_Visible;
    }
    public char LettreVisible
{
    get { return lettreVisible; }
}
public De(char[] lettres)
{
    if (lettres.Length != 6)
    {
        throw new ArgumentException("Un dé doit avoir exactement 6 faces.");
    }
    faces = lettres;
    lettreVisible = faces[0];
}
public void Lance(Random r)
{
    lettreVisible = faces[r.Next(6)];
}
public override string ToString()
{
    return $"Dé [Faces: {string.Join(", ", faces)}] - Lettre visible: {lettreVisible}";
}
}
