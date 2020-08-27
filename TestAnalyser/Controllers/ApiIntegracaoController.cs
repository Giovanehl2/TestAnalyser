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
    public class ApiIntegracaoController : Controller
    {
        public static Aluno aluno = new Aluno();
        public static Professor professor = new Professor();
        public static Disciplina disciplina = new Disciplina();
        public static Curso curso = new Curso();
        public static Turma turma = new Turma();
        public static Usuario usr = new Usuario();

        public static List<Aluno> alunos = new List<Aluno>();

        public static Aluno alunoEdit = new Aluno();
        public static Professor professorEdit = new Professor();
        public static Disciplina disciplinaEdit = new Disciplina();
        public static Curso cursoEdit = new Curso();
        public static Turma turmaEdit = new Turma();
        public static Usuario usrEdit = new Usuario();


        public static Boolean RelacionarObj(ObjApi objApi)
        {
            try
            {
                alunos.Clear();
                /*Disciplina*/
                disciplinaEdit = new Disciplina();
                professorEdit = new Professor();

                disciplinaEdit = DisciplinaDAO.BuscarPorNome(disciplina.Nome);

                cursoEdit = CursoDAO.BuscarPorNome(curso.NomeCurso);
                turmaEdit = TurmaDAO.BuscarTurmaNome(turma.NomeTurma);
                professorEdit = ProfessorDAO.BuscarProfessorMatricula(professor.Matricula);

                disciplinaEdit.Turmas.Add(turmaEdit);
                disciplinaEdit.Cursos.Add(cursoEdit);
                disciplinaEdit.Professores.Add(professorEdit);

                /*edição para inclusão de uma Disciplina para o Professor*/
                DisciplinaDAO.EditarDisciplina(disciplinaEdit);


                /*Turma*/
                turmaEdit = new Turma();
                disciplinaEdit = new Disciplina();
                alunoEdit = new Aluno();

                disciplinaEdit = DisciplinaDAO.BuscarPorNome(disciplina.Nome);
                turmaEdit = TurmaDAO.BuscarTurmaNome(turma.NomeTurma);
                cursoEdit = CursoDAO.BuscarPorNome(curso.NomeCurso);

                foreach (AlunoJson item in objApi.AlunoJson)
                {
                    alunos.Add(AlunoDAO.BuscarAlunoPorMatricula(item.Matricula));
                }


                turmaEdit.Curso = cursoEdit;
                turmaEdit.Alunos = alunos; //insere todos alunos de uma vez
                turmaEdit.Disciplinas.Add(disciplinaEdit);

                TurmaDAO.EditarTurma(turmaEdit);

                /*Curso*/
                cursoEdit = new Curso();
                turmaEdit = new Turma();
                disciplinaEdit = new Disciplina();

                cursoEdit = CursoDAO.BuscarPorNome(curso.NomeCurso);
                disciplinaEdit = DisciplinaDAO.BuscarPorNome(disciplina.Nome);
                turmaEdit = TurmaDAO.BuscarTurmaNome(turma.NomeTurma);
                cursoEdit.Turmas.Add(turmaEdit);
                cursoEdit.Disciplinas.Add(disciplinaEdit);

                CursoDAO.EditarCurso(cursoEdit);




                /* Aluno*/
                turmaEdit = new Turma();
                turmaEdit = TurmaDAO.BuscarTurmaNome(turma.NomeTurma);

                foreach (AlunoJson item in objApi.AlunoJson)
                {
                    alunoEdit = new Aluno();

                    alunoEdit = AlunoDAO.BuscarAlunoPorMatricula(item.Matricula);
                    alunoEdit.Turmas.Add(turmaEdit);

                    /*edição para inclusão de uma turma para o aluno*/
                    AlunoDAO.EditarAluno(alunoEdit);
                }



                /*Professor*/
                usrEdit = new Usuario();
                disciplinaEdit = new Disciplina();
                professorEdit = new Professor();

                professorEdit = ProfessorDAO.BuscarProfessorMatricula(professor.Matricula);
                disciplinaEdit = DisciplinaDAO.BuscarPorNome(disciplina.Nome);
                professorEdit.Disciplinas.Add(disciplinaEdit);

                /*edição para inclusão de uma Disciplina para o Professor*/
                ProfessorDAO.EditarProfessor(professorEdit);
            }

            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);
                return false;
            }
            return true;

        }
        public static Boolean OrganizarObjParaPersistir(ObjApi objApi)
        {
            try
            {
                foreach (AlunoJson objAluno in objApi.AlunoJson)
                {
                    aluno = new Aluno();

                    aluno.CPF = objAluno.CPF;
                    aluno.Nome = objAluno.Nome;
                    aluno.Email = objAluno.Email;
                    aluno.Matricula = objAluno.Matricula;
                    //aluno.Provas = null;
                    aluno.Turmas = null;

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



                usr = new Usuario();

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

                disciplina.Nome = objApi.DisciplinaJson.Nome;
                disciplina.Descricao = objApi.DisciplinaJson.Descricao;
                disciplina.Turmas = null;

                /*verifica se o registro ja se encontra na base*/
                if (DisciplinaDAO.BuscarPorNome(disciplina.Nome) == null)
                {
                    /*persiste disciplina*/
                    DisciplinaDAO.CadastrarDisciplina(disciplina);
                }

                turma.NomeTurma = objApi.TurmaJson.Nome;
                turma.Periodo = objApi.TurmaJson.Periodo;
                turma.Curso = curso;
                turma.Curso = null;
                turma.Alunos = null;
                turma.Disciplinas = null;

                /*verifica se o registro ja se encontra na base*/
                if (TurmaDAO.BuscarTurmaNome(turma.NomeTurma) == null)
                {
                    TurmaDAO.CadastrarTurma(turma);

                }
                curso.NomeCurso = objApi.CursoJson.Nome;
                curso.Descricao = objApi.CursoJson.Descricao;
                curso.Turmas = null;/*preencher depois*/

                /*verifica se o registro ja se encontra na base*/
                if (CursoDAO.BuscarPorNome(curso.NomeCurso) == null)
                {
                    /*persiste Curso*/
                    CursoDAO.CadastrarCurso(curso);

                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);
                return false;
            }
            return true;
        }
        public static void Importar()
        {
            List<ObjApi> objApi = new List<ObjApi>();


            using (var client = new WebClient { Encoding = System.Text.Encoding.UTF8 })
            {
                String json = client.DownloadString("http://localhost:44351/api/values");
                var serializer = new JavaScriptSerializer();

                json = json.TrimStart('\"');
                json = json.TrimEnd('\"');
                json = json.Replace("\\", "");
                objApi = JsonConvert.DeserializeObject<List<ObjApi>>(json);

                foreach (ObjApi item in objApi)
                {

                    /*posteriormente podemos realizar o log dos itens registrados ou não no import*/
                    OrganizarObjParaPersistir(item);
                    RelacionarObj(item);


                }

            }

        }
    }
}