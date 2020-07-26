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


        public static void OrganizarObjParaPersistir()
        {
            Aluno aluno = new Aluno();
            Professor professor = new Professor();
            Disciplina disciplina = new Disciplina();
            Curso curso = new Curso();
            Turma turma = new Turma();
            Usuario usr = new Usuario();


            Aluno alunoEdit = new Aluno();
            Professor professorEdit = new Professor();
            Disciplina disciplinaEdit = new Disciplina();
            Curso cursoEdit = new Curso();
            Turma turmaEdit = new Turma();
            Usuario usrEdit = new Usuario();


            AlunoJson alunoJson = new AlunoJson();
            ProfessorJson professorJson = new ProfessorJson();
            DisciplinaJson disciplinaJson = new DisciplinaJson();
            CursoJson cursoJson = new CursoJson();
            TurmaJson turmaJson = new TurmaJson();


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



            /* Aluno*/

            usrEdit = UsuarioDAO.BuscarUsuarioPorLogin(Convert.ToString(aluno.Matricula));

            turmaEdit = TurmaDAO.BuscarTurmaNome(turma.NomeTurma);

            usrEdit.Aluno.Turmas.Add(turmaEdit);

            /*edição para inclusão de uma turma para o aluno*/
            UsuarioDAO.EditarUsuario(usrEdit);



            /*Professor*/
            usrEdit = new Usuario();

            usrEdit = UsuarioDAO.BuscarUsuarioPorLogin(Convert.ToString(professor.Matricula));

            disciplinaEdit = DisciplinaDAO.BuscarPorNome(disciplina.nome);

            usrEdit.Professor.Disciplinas.Add(disciplinaEdit);

            /*edição para inclusão de uma Disciplina para o Professor*/
            UsuarioDAO.EditarUsuario(usrEdit);



            /*Disciplina*/
            disciplinaEdit = new Disciplina();

            disciplinaEdit = DisciplinaDAO.BuscarPorNome(disciplina.nome);

            cursoEdit = CursoDAO.BuscarPorNome(curso.NomeCurso);
            turmaEdit = TurmaDAO.BuscarTurmaNome(disciplina.nome);

            disciplinaEdit.Turmas.Add(turmaEdit);
            disciplinaEdit.Cursos.Add(cursoEdit);

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



        }
    }
}