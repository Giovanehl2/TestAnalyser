using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAnalyser.DAL;
using TestAnalyser.Model;

namespace TestAnalyser.Controllers
{
    public class ApiIntegracaoController
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

        public static AlunoJson alunoJson = new AlunoJson();
        public static ProfessorJson professorJson = new ProfessorJson();
        public static DisciplinaJson disciplinaJson = new DisciplinaJson();
        public static CursoJson cursoJson = new CursoJson();
        public static TurmaJson turmaJson = new TurmaJson();

        public static void OrganizarObjParaPersistir()
        {



            alunoJson.CPF = "08107775210";
            alunoJson.Nome = "giovane";
            alunoJson.Email = "giovanehl2@gmail.com";
            alunoJson.Matricula = 1608540;


            professorJson.Matricula = 15044;
            professorJson.Nome = "juka";
            professorJson.CPF = "0411108902";
            professorJson.Email = "juka@gmail.com";

            disciplinaJson.DisciplinaId = 446;
            disciplinaJson.nome = "arquitetura";
            disciplinaJson.descrição = "materia focada em componentes de hardware";


            cursoJson.nomeCurso = "Analise e desenvolvimento de sistemas";
            cursoJson.descricao = "curso de tecnologia voltado a area de tecnologia";

            turmaJson.NomeTurma = "a45";
            turmaJson.Periodo = "quarto periodo";


            aluno.CPF = alunoJson.CPF;
            aluno.Nome = alunoJson.Nome;
            aluno.Email = alunoJson.Email;
            aluno.Matricula = alunoJson.Matricula;
            aluno.Provas = null;
            aluno.Turmas = null;/*preencher depois*/

            usr.Login = Convert.ToString(aluno.Matricula);
            usr.Senha = null;
            usr.TipoUsr = 1;
            usr.Aluno = aluno;
            usr.Professor = null;
            usr.Admin = null;

            /*persiste usuario - Aluno*/
            UsuarioDAO.CadastrarUsuario(usr);

            usr = new Usuario();

            professor.Matricula = professorJson.Matricula;
            professor.Nome = professorJson.Nome;
            professor.CPF = professorJson.CPF;
            professor.Email = professorJson.Email;
            professor.Provas = null;
            professor.Disciplinas = null;/*preencher depois*/


            usr.Login = Convert.ToString(professor.Matricula);
            usr.Senha = null;
            usr.TipoUsr = 1;
            usr.Professor = professor;
            usr.Aluno = null;
            usr.Admin = null;

            /*persiste usuario - Professor*/
            UsuarioDAO.CadastrarUsuario(usr);


            disciplina.DisciplinaId = disciplinaJson.DisciplinaId;
            disciplina.nome = disciplinaJson.nome;
            disciplina.descrição = disciplinaJson.descrição;
            disciplina.Turmas = null;/*preencher depois*/

            /*persiste disciplina*/
            DisciplinaDAO.CadastrarDisciplina(disciplina);


            turma.NomeTurma = turmaJson.NomeTurma;
            turma.Periodo = turmaJson.Periodo;
            turma.Curso = curso;
            turma.Curso = null;
            turma.Alunos = null;
            turma.Disciplinas = null; /*preencher depois*/

            TurmaDAO.CadastrarTurma(turma);

            curso.NomeCurso = cursoJson.nomeCurso;
            curso.Descricao = cursoJson.descricao;
            curso.Turmas = null;/*preencher depois*/

            /*persiste Curso*/
            CursoDAO.CadastrarCurso(curso);


            fazerLigacoes();




        }
        public static void fazerLigacoes()
        {


            /*Disciplina*/
            disciplinaEdit = new Disciplina();

            disciplinaEdit = DisciplinaDAO.BuscarPorNome(disciplina.nome);

            cursoEdit = CursoDAO.BuscarPorNome(curso.NomeCurso);
            turmaEdit = TurmaDAO.BuscarTurmaNome(turma.NomeTurma);
            usrEdit = UsuarioDAO.BuscarUsuarioPorLogin(Convert.ToString(professor.Matricula));

            disciplinaEdit.Turmas.Add(turmaEdit);
            disciplinaEdit.Cursos.Add(cursoEdit);
            disciplinaEdit.Professores.Add(usrEdit.Professor);

            /*edição para inclusão de uma Disciplina para o Professor*/
            DisciplinaDAO.EditarDisciplina(disciplinaEdit);


            /*Turma*/
            turmaEdit = new Turma();
            disciplinaEdit = new Disciplina();
            usrEdit = new Usuario();

            disciplinaEdit = DisciplinaDAO.BuscarPorNome(disciplina.nome);
            turmaEdit = TurmaDAO.BuscarTurmaNome(turma.NomeTurma);
            cursoEdit = CursoDAO.BuscarPorNome(curso.NomeCurso);
            usrEdit = UsuarioDAO.BuscarUsuarioPorLogin(Convert.ToString(aluno.Matricula));

            turmaEdit.Curso = cursoEdit;
            turmaEdit.Alunos.Add(usrEdit.Aluno);
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

            usrEdit = UsuarioDAO.BuscarUsuarioPorLogin(Convert.ToString(aluno.Matricula));

            turmaEdit = TurmaDAO.BuscarTurmaNome(turma.NomeTurma);

            usrEdit.Aluno.Turmas.Add(turmaEdit);

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
    }
}