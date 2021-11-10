using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public void Inserir(Usuario u)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                if(u.Senha !=null){
                    u.Senha = GerarHashMd5(u.Senha);
                }
                bc.Usuarios.Add(u);
                bc.SaveChanges();
            }
        }

        public static string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public void Atualizar(Usuario u)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario usuario = bc.Usuarios.Find(u.Id);
                usuario.Login = u.Login;
                if(u.Senha !=null){
                    u.Senha = GerarHashMd5(u.Senha);
                }
                usuario.DataNascimento = u.DataNascimento;

                bc.SaveChanges();
            }
        }

         public bool ValidarLogin(Usuario user)
        {
            IQueryable<Usuario> usuarios = null;
            int resultado =0 ;
            using (BibliotecaContext bc = new BibliotecaContext())
            {
               usuarios = bc.Usuarios.Where(u => u.Login.Contains(user.Login) && u.Senha.Contains(GerarHashMd5(user.Senha)));
               resultado = usuarios.ToList().Count;
            }
            return resultado>0;
        }





        public ICollection<Usuario> ListarTodos()
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                IQueryable<Usuario> query;

                //ordenação padrão
                query = bc.Usuarios;
                return query.OrderBy(l => l.Login).ToList();
            }
        }



        public Usuario ObterPorId(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                return bc.Usuarios.Find(id);
            }
        }

        public void Remover(int id)
        {
            using (BibliotecaContext bc = new BibliotecaContext())
            {
                Usuario usuario = ObterPorId(id);
                bc.Usuarios.Remove(usuario);
            }
        }
    }
}