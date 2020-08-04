using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

                /*Disciplina*/
                disciplinaEdit = new Disciplina();
                professorEdit = new Professor();

                disciplinaEdit = DisciplinaDAO.BuscarPorNome(disciplina.nome);

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

                disciplinaEdit = DisciplinaDAO.BuscarPorNome(disciplina.nome);
                turmaEdit = TurmaDAO.BuscarTurmaNome(turma.NomeTurma);
                cursoEdit = CursoDAO.BuscarPorNome(curso.NomeCurso);
                alunoEdit = AlunoDAO.BuscarAlunoPorMatricula(Convert.ToString(aluno.Matricula));

                turmaEdit.Curso = cursoEdit;
                turmaEdit.Alunos.Add(alunoEdit);
                turmaEdit.Disciplinas.Add(disciplinaEdit);

                TurmaDAO.EditarTurma(turmaEdit);

                /*Curso*/
                cursoEdit = new Curso();
                turmaEdit = new Turma();
                disciplinaEdit = new Disciplina();

                cursoEdit = CursoDAO.BuscarPorNome(curso.NomeCurso);
                disciplinaEdit = DisciplinaDAO.BuscarPorNome(disciplina.nome);
                turmaEdit = TurmaDAO.BuscarTurmaNome(turma.NomeTurma);
                cursoEdit.Turmas.Add(turmaEdit);
                cursoEdit.Disciplinas.Add(disciplinaEdit);

                CursoDAO.EditarCurso(cursoEdit);

                /* Aluno*/
                alunoEdit = new Aluno();
                turmaEdit = new Turma();

                alunoEdit = AlunoDAO.BuscarAlunoPorMatricula(Convert.ToString(aluno.Matricula));
                turmaEdit = TurmaDAO.BuscarTurmaNome(turma.NomeTurma);
                alunoEdit.Turmas.Add(turmaEdit);

                /*edição para inclusão de uma turma para o aluno*/
                UsuarioDAO.EditarUsuario(usrEdit);


                /*Professor*/
                usrEdit = new Usuario();
                disciplinaEdit = new Disciplina();
                professorEdit = new Professor();

                professorEdit = ProfessorDAO.BuscarProfessorMatricula(professor.Matricula);
                disciplinaEdit = DisciplinaDAO.BuscarPorNome(disciplina.nome);
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

                aluno.CPF = objApi.AlunoJson.CPF;
                aluno.Nome = objApi.AlunoJson.Nome;
                aluno.Email = objApi.AlunoJson.Email;
                aluno.Matricula = objApi.AlunoJson.Matricula;
                aluno.Provas = null;
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


                usr = new Usuario();

                professor.Matricula = objApi.ProfessorJson.Matricula;
                professor.Nome = objApi.ProfessorJson.Nome;
                professor.CPF = objApi.ProfessorJson.CPF;
                professor.Email = objApi.ProfessorJson.Email;
                professor.Provas = null;
                professor.Disciplinas = null;


                usr.Login = Convert.ToString(professor.Matricula);
                usr.Senha = null;
                usr.TipoUsr = 1;
                usr.Professor = professor;
                usr.Aluno = null;
                usr.Admin = null;

                /*verifica se o registro ja se encontra na base*/
                if (UsuarioDAO.BuscarUsuarioPorLogin(usr.Login) == null)
                {
                    /*persiste usuario - Professor*/
                    UsuarioDAO.CadastrarUsuario(usr);
                }

                disciplina.DisciplinaId = objApi.DisciplinaJson.DisciplinaId;
                disciplina.nome = objApi.DisciplinaJson.nome;
                disciplina.descrição = objApi.DisciplinaJson.descrição;
                disciplina.Turmas = null;

                /*verifica se o registro ja se encontra na base*/
                if (DisciplinaDAO.BuscarPorNome(disciplina.nome) == null)
                {
                    /*persiste disciplina*/
                    DisciplinaDAO.CadastrarDisciplina(disciplina);
                }

                turma.NomeTurma = objApi.TurmaJson.NomeTurma;
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
                curso.NomeCurso = objApi.CursoJson.nomeCurso;
                curso.Descricao = objApi.CursoJson.descricao;
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
        public ActionResult importar()
        {
            List<ObjApi> objApi = new List<ObjApi>();


            // List<Investimento> invest = new List<Investimento>();
            using (var client = new WebClient())
            {
                String json = client.DownloadString("http://localhost:51484/api/values");
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
            return RedirectToAction("Index", "Investimento");
        }
    }
}