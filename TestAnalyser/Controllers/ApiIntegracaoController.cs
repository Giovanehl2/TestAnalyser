using iTextSharp.LGPLv2.Core.System.NetUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TestAnalyser.DAL;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{
    [Authorize]
    public class ApiIntegracaoController : Controller
    {
        public static Aluno aluno = new Aluno();
        public static Professor professor = new Professor();
        public static Disciplina disciplina = new Disciplina();
        public static Curso curso = new Curso();
        public static Turma turma = new Turma();
        public static Usuario usr = new Usuario();

        public static Aluno alunoEdit = new Aluno();
        public static Professor professorEdit = new Professor();
        public static Disciplina disciplinaEdit = new Disciplina();
        public static Curso cursoEdit = new Curso();
        public static Turma turmaEdit = new Turma();
        public static Usuario usrEdit = new Usuario();



        public static Boolean OrganizarObjParaPersistir(ObjApi objApi)
        {
            try
            {
                /* persiste o objeto vindo da api*/
                CadastrarAluno(objApi);
                CadastrarProfessor(objApi);
                CadastrarCurso(objApi);
                CadastrarTurma(objApi);
                CadastrarDisciplina(objApi);


            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return false;
            }
            return true;
        }
        public static bool Importar(Configuracao config)
        {
            List<ObjApi> objApi = new List<ObjApi>();
            bool sucesso = true;

            using (var client = new WebClient { Encoding = System.Text.Encoding.UTF8 })
            {
                String json = "";
                if (config == null)
                {
                      json = client.DownloadString("http://localhost:44351/api/values");
                }
                else
                {
                    //carrega url da base
                     json = client.DownloadString(config.UrlApi);
                }

                var serializer = new JavaScriptSerializer();

                json = json.TrimStart('\"');
                json = json.TrimEnd('\"');
                json = json.Replace("\\", "");
                objApi = JsonConvert.DeserializeObject<List<ObjApi>>(json);

                foreach (ObjApi item in objApi)
                {
                    /*posteriormente podemos realizar o log dos itens registrados ou não no import*/
                    sucesso = OrganizarObjParaPersistir(item);
                    sucesso = RelacionarObj(item);
                }

            }
            return sucesso;

        }
       
        public static bool RelacionarObj(ObjApi objApi)
        {
            try
            {
                ManipularAluno(objApi);
                ManipularDisciplina(objApi);
                ManipularProfessor(objApi);
                ManipularTurma(objApi);
                ManipularCurso(objApi);

            }

            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);
                return false;
            }
            return true;

        }
        private static void CadastrarAluno(ObjApi objApi)
        {
            aluno = new Aluno();
            usr = new Usuario();

            aluno.CPF = objApi.AlunoJson.CPF;
            aluno.Nome = objApi.AlunoJson.Nome;
            aluno.Email = objApi.AlunoJson.Email;
            aluno.Matricula = objApi.AlunoJson.Matricula;
            aluno.Turmas = null;
            aluno.Disciplinas = null;

            usr.Login = Convert.ToString(aluno.Matricula);
            usr.Senha = null;
            usr.TipoUsr = 1;
            usr.Aluno = aluno;
            usr.Professor = null;
            usr.Admin = null;


            /*verifica se o registro ja se encontra na base*/
            if (UsuarioDAO.BuscarUsuarioPorLogin(usr.Login) == null)
            {
                /*persiste usuario - Aluno*/
                UsuarioDAO.CadastrarUsuario(usr);
            }

        }

        private static void CadastrarProfessor(ObjApi objApi)
        {
            usr = new Usuario();
            professor = new Professor();

            professor.Matricula = objApi.ProfessorJson.Matricula;
            professor.Nome = objApi.ProfessorJson.Nome;
            professor.CPF = objApi.ProfessorJson.CPF;
            professor.Email = objApi.ProfessorJson.Email;
            professor.Provas = null;
            professor.Disciplinas = null;


            usr.Login = Convert.ToString(professor.Matricula);
            usr.Senha = null;
            usr.TipoUsr = 2;
            usr.Professor = professor;
            usr.Aluno = null;
            usr.Admin = null;

            /*verifica se o registro ja se encontra na base*/
            if (UsuarioDAO.BuscarUsuarioPorLogin(usr.Login) == null)
            {
                /*persiste usuario - Professor*/
                UsuarioDAO.CadastrarUsuario(usr);
            }
        }


        private static void CadastrarDisciplina(ObjApi objApi)
        {
            disciplina = new Disciplina();

            disciplina.Nome = objApi.DisciplinaJson.Nome;
            disciplina.Descricao = objApi.DisciplinaJson.Descricao;
            disciplina.Turmas = null;
            disciplina.Professores = null;
            disciplina.Alunos = null;

            /*verifica se o registro ja se encontra na base*/
            if (DisciplinaDAO.BuscarPorNome(disciplina.Nome) == null)
            {
                /*persiste disciplina*/
                DisciplinaDAO.CadastrarDisciplina(disciplina);
            }
        }
        private static void CadastrarTurma(ObjApi objApi)
        {
            turma = new Turma();

            turma.NomeTurma = objApi.TurmaJson.Nome;
            turma.Periodo = objApi.TurmaJson.Periodo;
            turma.Curso = null;
            turma.Alunos = null;
            turma.Disciplinas = null;

            /*verifica se o registro ja se encontra na base*/
            if (TurmaDAO.BuscarTurmaNome(turma.NomeTurma) == null)
            {
                TurmaDAO.CadastrarTurma(turma);

            }

        }

        private static void CadastrarCurso(ObjApi objApi)
        {
            curso = new Curso();

            curso.NomeCurso = objApi.CursoJson.Nome;
            curso.Descricao = objApi.CursoJson.Descricao;
            curso.Disciplinas = null;
            curso.Turmas = null;

            /*verifica se o registro ja se encontra na base*/
            if (CursoDAO.BuscarPorNome(curso.NomeCurso) == null)
            {
                /*persiste Curso*/
                CursoDAO.CadastrarCurso(curso);

            }
        }

        private static void ManipularTurma(ObjApi objApi)
        {
            turmaEdit = new Turma();
            disciplinaEdit = new Disciplina();
            alunoEdit = new Aluno();
            cursoEdit = new Curso();

            disciplinaEdit = DisciplinaDAO.BuscarPorNome(objApi.DisciplinaJson.Nome);
            turmaEdit = TurmaDAO.BuscarTurmaNome(objApi.TurmaJson.Nome);
            cursoEdit = CursoDAO.BuscarPorNome(objApi.CursoJson.Nome);


            alunoEdit = AlunoDAO.BuscarAlunoPorMatricula(objApi.AlunoJson.Matricula);

            turmaEdit.Curso = cursoEdit;

            //flag para fazer a validação de inclusão de registro
            bool cadastrar = true;
            if (turmaEdit.Alunos != null && turmaEdit.Alunos.Count() > 1)
            {
                foreach (Aluno obj in turmaEdit.Alunos)
                {
                    if (obj.Matricula.Equals(alunoEdit.Matricula)){
                        cadastrar = false;
                        break;
                    }
                        
                }

            }
            if (cadastrar)
            turmaEdit.Alunos.Add(alunoEdit);


            cadastrar = true;

            if (turmaEdit.Disciplinas != null && turmaEdit.Disciplinas.Count() > 1)
            {
                foreach (Disciplina obj in turmaEdit.Disciplinas)
                {
                    if (obj.Nome.Equals(disciplinaEdit.Nome))
                    {
                        cadastrar = false;
                        break;
                    }

                }

            }
            if (cadastrar)
                turmaEdit.Disciplinas.Add(disciplinaEdit);


            TurmaDAO.EditarTurma(turmaEdit);

        }

        private static void ManipularCurso(ObjApi objApi)
        {

            cursoEdit = new Curso();
            turmaEdit = new Turma();
            disciplinaEdit = new Disciplina();

            cursoEdit = CursoDAO.BuscarPorNome(objApi.CursoJson.Nome);
            disciplinaEdit = DisciplinaDAO.BuscarPorNome(objApi.DisciplinaJson.Nome);
            turmaEdit = TurmaDAO.BuscarTurmaNome(objApi.TurmaJson.Nome);

            //flag para fazer a validação de inclusão de registro
            bool cadastrar = true;
            if (cursoEdit.Turmas != null && cursoEdit.Turmas.Count() > 1)
            {
                foreach (Turma obj in cursoEdit.Turmas)
                {
                    if (obj.NomeTurma.Equals(turmaEdit.NomeTurma))
                    {
                        cadastrar = false;
                        break;
                    }

                }

            }
            if (cadastrar)
                cursoEdit.Turmas.Add(turmaEdit);


            //flag para fazer a validação de inclusão de registro
            cadastrar = true;
            if (cursoEdit.Disciplinas != null && cursoEdit.Disciplinas.Count() > 1)
            {
                foreach (Disciplina obj in cursoEdit.Disciplinas)
                {
                    if (obj.Nome.Equals(disciplinaEdit.Nome))
                    {
                        cadastrar = false;
                        break;
                    }

                }

            }
            if (cadastrar)
            cursoEdit.Disciplinas.Add(disciplinaEdit);

            CursoDAO.EditarCurso(cursoEdit);

        }
        private static void ManipularAluno(ObjApi objApi)
        {
            turmaEdit = new Turma();
            alunoEdit = new Aluno();
            disciplinaEdit = new Disciplina();
            cursoEdit = new Curso();
            professorEdit = new Professor();

            turmaEdit = TurmaDAO.BuscarTurmaNome(objApi.TurmaJson.Nome);
            alunoEdit = AlunoDAO.BuscarAlunoPorMatricula(objApi.AlunoJson.Matricula);
            disciplinaEdit = DisciplinaDAO.BuscarPorNome(objApi.DisciplinaJson.Nome);
            cursoEdit = CursoDAO.BuscarPorNome(objApi.CursoJson.Nome);
            professorEdit = ProfessorDAO.BuscarProfessorMatricula(objApi.ProfessorJson.Matricula);

            //flag para fazer a validação de inclusão de registro
            bool cadastrar = true;
            if (turmaEdit.Disciplinas != null && turmaEdit.Disciplinas.Count() > 1)
            {
                foreach (Disciplina obj in turmaEdit.Disciplinas)
                {
                    if (obj.Nome.Equals(disciplinaEdit.Nome))
                    {
                        cadastrar = false;
                        break;
                    }

                }

            }
            if (cadastrar)
            turmaEdit.Disciplinas.Add(disciplinaEdit);

            cadastrar = true;
            if (disciplinaEdit.Alunos != null && disciplinaEdit.Alunos.Count() > 1)
            {
                foreach (Aluno obj in disciplinaEdit.Alunos)
                {
                    if (obj.Nome.Equals(alunoEdit.Nome))
                    {
                        cadastrar = false;
                        break;
                    }

                }

            }
            if (cadastrar)
                disciplinaEdit.Alunos.Add(alunoEdit);


            cadastrar = true;
            if (disciplinaEdit.Turmas != null && disciplinaEdit.Turmas.Count() > 1)
            {
                foreach (Turma obj in disciplinaEdit.Turmas)
                {
                    if (obj.NomeTurma.Equals(turmaEdit.NomeTurma))
                    {
                        cadastrar = false;
                        break;
                    }

                }

            }
            if (cadastrar)
                disciplinaEdit.Turmas.Add(turmaEdit);


            cadastrar = true;
            if (disciplinaEdit.Professores != null && disciplinaEdit.Professores.Count() > 1)
            {
                foreach (Professor obj in disciplinaEdit.Professores)
                {
                    if (obj.Matricula.Equals(professorEdit.Matricula))
                    {
                        cadastrar = false;
                        break;
                    }

                }

            }
            if (cadastrar)
                disciplinaEdit.Professores.Add(professorEdit);



            cadastrar = true;
            if (disciplinaEdit.Cursos != null && disciplinaEdit.Cursos.Count() > 1)
            {
                foreach (Curso obj in disciplinaEdit.Cursos)
                {
                        if (obj.NomeCurso.Equals(cursoEdit.NomeCurso))
                        {
                            cadastrar = false;
                            break;
                        }
                }

            }
            if (cadastrar)
                disciplinaEdit.Cursos.Add(cursoEdit);


            cadastrar = true;
            if (alunoEdit.Turmas != null && alunoEdit.Turmas.Count() > 1)
            {
                foreach (Turma obj in alunoEdit.Turmas)
                {
                    if (obj.NomeTurma.Equals(turmaEdit.NomeTurma))
                    {
                        cadastrar = false;
                        break;
                    }
                }

            }
            if (cadastrar)
                alunoEdit.Turmas.Add(turmaEdit);



            disciplinaEdit.Provas = null;
            
            alunoEdit.Disciplinas = new List<Disciplina>();


            cadastrar = true;
            if (alunoEdit.Disciplinas != null && alunoEdit.Disciplinas.Count() > 1)
            {
                foreach (Disciplina obj in alunoEdit.Disciplinas)
                {
                    if (obj.Nome.Equals(disciplinaEdit.Nome))
                    {
                        cadastrar = false;
                        break;
                    }
                }

            }
            if (cadastrar)
                alunoEdit.Disciplinas.Add(disciplinaEdit);



            AlunoDAO.EditarAluno(alunoEdit);
        }
        private static void ManipularProfessor(ObjApi objApi)
        {
            usrEdit = new Usuario();
            disciplinaEdit = new Disciplina();
            professorEdit = new Professor();

            professorEdit = ProfessorDAO.BuscarProfessorMatricula(objApi.ProfessorJson.Matricula);
            disciplinaEdit = DisciplinaDAO.BuscarPorNome(objApi.DisciplinaJson.Nome);

            bool cadastrar = true;
            if (professorEdit.Disciplinas != null && professorEdit.Disciplinas.Count() > 1)
            {
                foreach (Disciplina obj in professorEdit.Disciplinas)
                {
                    if (obj.Nome.Equals(disciplinaEdit.Nome))
                    {
                        cadastrar = false;
                        break;
                    }
                }
            }
            if (cadastrar)
            professorEdit.Disciplinas.Add(disciplinaEdit);

            /*edição para inclusão de uma Disciplina para o Professor*/
            ProfessorDAO.EditarProfessor(professorEdit);
        }

        private static void ManipularDisciplina(ObjApi objApi)
        {
            /*Disciplina*/
            disciplinaEdit = new Disciplina();
            professorEdit = new Professor();
            alunoEdit = new Aluno();
            cursoEdit = new Curso();

            alunoEdit = AlunoDAO.BuscarAlunoPorMatricula(objApi.AlunoJson.Matricula);
            disciplinaEdit = DisciplinaDAO.BuscarPorNome(objApi.DisciplinaJson.Nome);

            cursoEdit = CursoDAO.BuscarPorNome(objApi.CursoJson.Nome);
            turmaEdit = TurmaDAO.BuscarTurmaNome(objApi.TurmaJson.Nome);
            professorEdit = ProfessorDAO.BuscarProfessorMatricula(objApi.ProfessorJson.Matricula);



            bool cadastrar = true;
            if (disciplinaEdit.Turmas != null && disciplinaEdit.Turmas.Count() > 1)
            {
                foreach (Turma obj in disciplinaEdit.Turmas)
                {
                    if (obj.NomeTurma.Equals(turmaEdit.NomeTurma))
                    {
                        cadastrar = false;
                        break;
                    }
                }
            }
            if (cadastrar)
                disciplinaEdit.Turmas.Add(turmaEdit);


            cadastrar = true;
            if (disciplinaEdit.Cursos != null && disciplinaEdit.Cursos.Count() > 1)
            {
                foreach (Curso obj in disciplinaEdit.Cursos)
                {
                    if (obj.NomeCurso.Equals(cursoEdit.NomeCurso))
                    {
                        cadastrar = false;
                        break;
                    }
                }
            }
            if (cadastrar)
            disciplinaEdit.Cursos.Add(cursoEdit);


            cadastrar = true;
            if (disciplinaEdit.Professores != null && disciplinaEdit.Professores.Count() > 1)
            {
                foreach (Professor obj in disciplinaEdit.Professores)
                {
                    if (obj.Matricula.Equals(professorEdit.Matricula))
                    {
                        cadastrar = false;
                        break;
                    }
                }
            }
            if (cadastrar)
            disciplinaEdit.Professores.Add(professorEdit);


            /*edição para inclusão de uma Disciplina para o Professor*/
            DisciplinaDAO.EditarDisciplina(disciplinaEdit);
        }
    }
}