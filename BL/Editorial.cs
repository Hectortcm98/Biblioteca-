using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Editorial
    {
        public static(bool Success, string Message, List<ML.Editorial> editorials , Exception Error) GetAll()
        {
			try
			{
				using(DL.HcanalesLibrosJunio17Context Context = new DL.HcanalesLibrosJunio17Context())
				{
					var getEditorials = Context.Editorials.ToList();

					if(getEditorials != null )
					{
						List<ML.Editorial> editorials = new List<ML.Editorial>();
						foreach(var editorial in getEditorials)
						{
							ML.Editorial editorial1 = new ML.Editorial
							{
								IdEditorial = editorial.IdEditorial,
								Nombre = editorial.Nombre,
							};
							editorials.Add(editorial1);
						}
						return(true, null, editorials, null);
					}
                    return (false, "No hay ningún editorial", null, null);

                }
            }
			catch (Exception ex )
			{

				return (false, ex.Message, null, null);
			}
        }
    }
}
