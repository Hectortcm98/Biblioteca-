using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Libro
    {

        public static (bool Success, string Message, List<ML.Libro> libros, Exception Error ) GetAll()
        {
			try
			{
				using(DL.HcanalesLibrosJunio17Context Context = new DL.HcanalesLibrosJunio17Context())
				{
					var getLibros = (from lib in Context.Libros
									 join edi in Context.Editorials on lib.IdEditorial equals edi.IdEditorial
									 select new 
									 {
										 IdLibro = lib.IdLibro,
										 Autor = lib.Autor,
										 TituloLibro = lib.TituloLibro,
										 AñoPublicacion = lib.AñoPublicacion,
										 Imagen = lib.Imagen,
										 IdEditorial = lib.IdEditorial,
										 Nombre = edi.Nombre
									 });
					if( getLibros != null )
					{
						List<ML.Libro> libros = new List<ML.Libro>();
						foreach(var libro in getLibros )
						{
							ML.Libro libro1 = new ML.Libro
							{
								IdLibro = libro.IdLibro,
								Autor = libro.Autor,
								TituloLibro = libro.TituloLibro,
								AñoPublicacion = libro.AñoPublicacion,
								Imagen = libro.Imagen,

								Editorial = new ML.Editorial
								{
									IdEditorial = Convert.ToInt32(libro.IdEditorial),
									Nombre = libro.Nombre
								}
							};
							libros.Add( libro1 );

						}
						return (true, null, libros, null);
					}
                    return (false, "No se encontro ningun libro", null, null);
                }
			}
			catch (Exception ex)
			{

				return (false, ex.Message, null, null);
			}
        }


        public static (bool Success, string Message, ML.Libro Libro, Exception Error) GetById(int idLibro)
        {
            try
            {
                using (DL.HcanalesLibrosJunio17Context context = new DL.HcanalesLibrosJunio17Context())
                {
                    var getLibro = (from lib in context.Libros
                                       join edi in context.Editorials on lib.IdEditorial equals edi.IdEditorial
                                       where lib.IdLibro == idLibro
                                       select new
                                       {
 
                                           IdLibro = lib.IdLibro,
                                           Autor = lib.Autor,
                                           TituloLibro = lib.TituloLibro,
                                           AñoPublicacion = lib.AñoPublicacion,
                                           Imagen = lib.Imagen,
                                           IdEditorial = lib.IdEditorial,
                                           Nombre = edi.Nombre
                                       }
                                        ).SingleOrDefault();

                    if (getLibro != null)
                    {
                        ML.Libro libro = new ML.Libro
                        {
                            IdLibro = getLibro.IdLibro,
                            Autor = getLibro.Autor,
                            TituloLibro = getLibro.TituloLibro,
                            AñoPublicacion = getLibro.AñoPublicacion,
                            Imagen = getLibro.Imagen,
                            Editorial = new ML.Editorial
                            {
                                IdEditorial = getLibro.IdEditorial.Value,
                                Nombre = getLibro.Nombre,
                            }
                        };
                        return (true, null, libro, null);
                    }
                    return (false, "No se encontro el Libro", null, null);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, null, ex);
            }
        }


        public static (bool Success, string Message, Exception Error) Add(ML.Libro Libro)
        {
            try
            {
                using (DL.HcanalesLibrosJunio17Context context = new DL.HcanalesLibrosJunio17Context())
                {
                    DL.Libro LibroObj = new DL.Libro
                    {
                        IdLibro = Libro.IdLibro,
                        Autor = Libro.Autor,
                        TituloLibro = Libro.TituloLibro,
                        AñoPublicacion = Libro.AñoPublicacion,
                        Imagen = Libro.Imagen,
                        IdEditorial = Libro.Editorial.IdEditorial

                    };

                    context.Libros.Add(LibroObj);

                    int rowAffected = context.SaveChanges();

                    if (rowAffected > 0)
                    {
                        return (true, null, null);
                    }
                    else
                    {
                        return (false, "No se guardo el registro", null);
                    }
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, ex);
            }
        }


        public static (bool Success, string Message, Exception Error) Update(ML.Libro Libro)
        {
            try
            {
                using (DL.HcanalesLibrosJunio17Context context = new DL.HcanalesLibrosJunio17Context())
                {
                    DL.Libro LibroObj = new DL.Libro
                    {
                        IdLibro = Libro.IdLibro,
                        Autor = Libro.Autor,
                        TituloLibro = Libro.TituloLibro,
                        AñoPublicacion = Libro.AñoPublicacion,
                        Imagen = Libro.Imagen,
                        IdEditorial = Libro.Editorial.IdEditorial

                    };

                    context.Libros.Update(LibroObj);

                    int rowAffected = context.SaveChanges();

                    if (rowAffected > 0)
                    {
                        return (true, null, null);
                    }
                    else
                    {
                        return (false, "No actualizo el registro", null);
                    }
                }
            }
            catch (Exception ex)
            {

                return (false, ex.Message, ex);
            }
        }

        public static (bool Success, string Message, Exception Error) Delete(int IdLibro)
        {
            try
            {
                using (DL.HcanalesLibrosJunio17Context context = new DL.HcanalesLibrosJunio17Context())
                {
                    context.Libros.Remove(new DL.Libro { IdLibro = IdLibro });
                    int rowAffected = context.SaveChanges();

                    if (rowAffected > 0)
                    {
                        return (true, null, null);
                    }
                    else
                    {
                        return (false, "A ocurrido un error al eliminar el registro", null);
                    }
                }

            }
            catch (Exception ex)
            {

                return (false, ex.Message, ex);
            }
        }
    }
}
