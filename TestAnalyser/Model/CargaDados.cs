using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAnalyser.Model;

namespace TestAnalyser.Model
{
    public class CargaDados
    {
        public static List<AlunoJson> alunosTurmaA = new List<AlunoJson>();
        public static List<AlunoJson> alunosTurmaB = new List<AlunoJson>();
        public static ProfessorJson professor = new ProfessorJson();
        public static DisciplinaJson disciplina = new DisciplinaJson();
        public static CursoJson curso = new CursoJson();
        public static TurmaJson turma = new TurmaJson();
        public static Boolean dadosCarregado;
        public static AlunoJson alunoJson = new AlunoJson();
        public static ObjApi objApi = new ObjApi();
        public static List<ObjApi> listaApi = new List<ObjApi>();

        public static void CarregaTurmaA()
        {
            //turma A
            //orientação a objetos

            alunoJson = new AlunoJson();

            alunoJson.Matricula = 1605510;
            alunoJson.Nome = "Juca Almeida";
            alunoJson.CPF = "87187634480";
            alunoJson.Email = "juca.almeida@gmail.com";
            alunoJson.Situacao = "Ativo";
            alunosTurmaA.Add(alunoJson);

            alunoJson = new AlunoJson();

            alunoJson.Matricula = 1606610;
            alunoJson.Nome = "Ricardo de Freitas";
            alunoJson.CPF = "57347813395";
            alunoJson.Situacao = "Ativo";
            alunoJson.Email = "Ricardo.freitas@gmail.com";
            alunosTurmaA.Add(alunoJson);

            alunoJson = new AlunoJson();

            alunoJson.Matricula = 177668;
            alunoJson.Nome = "Pedro de azevedo";
            alunoJson.CPF = "86419425484";
            alunoJson.Email = "Pedro.azevedo@hotmail.com";
            alunosTurmaA.Add(alunoJson);


            alunoJson = new AlunoJson();

            alunoJson.Matricula = 188773;
            alunoJson.Nome = "katia de souza";
            alunoJson.CPF = "24436184633";
            alunoJson.Situacao = "Ativo";
            alunoJson.Email = "katia.souza@hotmail.com";
            alunosTurmaA.Add(alunoJson);


            alunoJson = new AlunoJson();

            alunoJson.Matricula = 188773;
            alunoJson.Nome = "rarize zanin";
            alunoJson.CPF = "28865694378";
            alunoJson.Situacao = "Ativo";
            alunoJson.Email = "rarize.zanin@bol.com";
            alunosTurmaA.Add(alunoJson);


            alunoJson = new AlunoJson();

            alunoJson.Matricula = 862448;
            alunoJson.Nome = "lucas garcia";
            alunoJson.CPF = "76881116500";
            alunoJson.Situacao = "Ativo";
            alunoJson.Email = "lucas.garcia@hotmail.com";
            alunosTurmaA.Add(alunoJson);

            alunoJson = new AlunoJson();

            alunoJson.Matricula = 147823;
            alunoJson.Nome = "andressa freitas";
            alunoJson.CPF = "46072721672";
            alunoJson.Situacao = "Ativo";
            alunoJson.Email = "andressa.freitas@hotmail.com";
            alunosTurmaA.Add(alunoJson);

            alunoJson = new AlunoJson();

            alunoJson.Matricula = 886671;
            alunoJson.Nome = "matheus de oliveira";
            alunoJson.CPF = "13417685540";
            alunoJson.Situacao = "Ativo";
            alunoJson.Email = "matheus.oliveira@hotmail.com";
            alunosTurmaA.Add(alunoJson);


            alunoJson = new AlunoJson();

            alunoJson.Matricula = 147243;
            alunoJson.Nome = "andressa zanin";
            alunoJson.CPF = "81878484648";
            alunoJson.Situacao = "Ativo";
            alunoJson.Email = "andressa.zanin@bol.com";
            alunosTurmaA.Add(alunoJson);


            alunoJson = new AlunoJson();

            alunoJson.Matricula = 17113;
            alunoJson.Nome = "maria claudia  feitosa";
            alunoJson.CPF = "19857628206";
            alunoJson.Situacao = "Ativo";
            alunoJson.Email = "maria.feitosa@bol.com";
            alunosTurmaA.Add(alunoJson);


        }

        public static void CarregaTurmaB()
        {
            //turma B

            AlunoJson aluno2 = new AlunoJson();

            aluno2.Matricula = 1617627;
            aluno2.Nome = "Kathelin D Agostin";
            aluno2.CPF = "55480719475";
            aluno2.Email = "kathelindagostin@gmail.com";
            alunosTurmaB.Add(aluno2);

            aluno2 = new AlunoJson();

            aluno2.Matricula = 1608540;
            aluno2.Nome = "Giovane Carvalho Reis";
            aluno2.CPF = "34286959252";
            aluno2.Email = "giovanehl2@gmail.com";
            alunosTurmaB.Add(aluno2);

            aluno2 = new AlunoJson();

            aluno2.Matricula = 1611889;
            aluno2.Nome = "Gabriel Rodrigues Broch";
            aluno2.CPF = "37915193104";
            aluno2.Email = "gabriel.broch98@gmail.com";
            alunosTurmaB.Add(aluno2);

            aluno2 = new AlunoJson();

            aluno2.Matricula = 1614785;
            aluno2.Nome = "Harrison Hakanen";
            aluno2.CPF = "03687098761";
            aluno2.Email = "harrisonhakinen@gmail.com";
            alunosTurmaB.Add(aluno2);

            aluno2 = new AlunoJson();

            aluno2.Matricula = 1619055;
            aluno2.Nome = "Daniela Bina Ferreira";
            aluno2.CPF = "85624617500";
            aluno2.Email = "danibinina@gmail.com";
            alunosTurmaB.Add(aluno2);

            aluno2 = new AlunoJson();

            aluno2.Matricula = 1508453;
            aluno2.Nome = "Felipe Pereira Augusto";
            aluno2.CPF = "92325728692";
            aluno2.Email = "felipe.bsi.up@gmail.com";
            alunosTurmaB.Add(aluno2);

            aluno2 = new AlunoJson();

            aluno2.Matricula = 1509693;
            aluno2.Nome = "Hudson Machado";
            aluno2.CPF = "58717762138";
            aluno2.Email = "hudson.machado@gmail.com";
            alunosTurmaB.Add(aluno2);

            aluno2 = new AlunoJson();

            aluno2.Matricula = 1609873;
            aluno2.Nome = "Alan Zequini";
            aluno2.CPF = "42202254471";
            aluno2.Email = "alan.zequini@gmail.com";
            alunosTurmaB.Add(aluno2);

            aluno2 = new AlunoJson();

            aluno2.Matricula = 1617624;
            aluno2.Nome = "Thaina Batista Carneiro";
            aluno2.CPF = "37912112340";
            aluno2.Email = "thaina.batista@gmail.com";
            alunosTurmaB.Add(aluno2);

            aluno2 = new AlunoJson();

            aluno2.Matricula = 15669;
            aluno2.Nome = "Pedro Becker";
            aluno2.CPF = "59571401552";
            aluno2.Email = "phgbecker@gmail.com";
            alunosTurmaB.Add(aluno2);


        }
        public static void cargaDeDados()
        {
            CarregaTurmaA();
            CarregaTurmaB();

            objApi = new ObjApi();

            CursoJson curso = new CursoJson();
            curso.Nome = "Sistemas de informação";
            curso.Descricao  = "Curso de tecnologia";
            curso.Situacao = "Ativo";


            // turma disciplina professor curso

            //turma A
            DisciplinaJson disciplina1 = new DisciplinaJson();

            disciplina1.Nome = "Orientação a objetos";
            disciplina1.Descricao = "desenvolvimento I";
            disciplina1.Situacao = "Ativa";


            ProfessorJson professor1 = new ProfessorJson();

            professor1.Nome = "Diogo deconto";
            professor1.CPF = "84423519810";
            professor1.Situacao = "Ativo";
            professor1.Email = "diogo.deconto@up.edu.br";
            professor1.Matricula = 1508510;


            TurmaJson turma = new TurmaJson();
            turma.TurmaId = 1;
            turma.Nome = "A";
            turma.Periodo = 4;

            objApi.AlunoJson = alunosTurmaA;
            objApi.CursoJson = curso;
            objApi.DisciplinaJson = disciplina1;
            objApi.ProfessorJson = professor1;
            objApi.TurmaJson = turma;

            listaApi.Add(objApi);

            // turma disciplina professor curso

            //turma B

            objApi = new ObjApi();
            turma = new TurmaJson();

            DisciplinaJson disciplina2 = new DisciplinaJson();

            disciplina2.Nome = "Arquitetura I";
            disciplina2.Descricao = " conceitos do funcionamento de um computador";
            disciplina2.Situacao = "Ativa";


            ProfessorJson professor2 = new ProfessorJson();

            professor2.Nome = "Leandro Andrade";
            professor2.CPF = "58221011436";
            professor2.Situacao = "Ativo";
            professor2.Email = "Leandro.andrade@up.edu.br";
            professor2.Matricula = 1704563;



            turma.TurmaId = 2;
            turma.Nome = "B";
            turma.Periodo = 4;


            objApi.AlunoJson = alunosTurmaB;
            objApi.CursoJson = curso;
            objApi.DisciplinaJson = disciplina2;
            objApi.ProfessorJson = professor2;
            objApi.TurmaJson = turma;

            listaApi.Add(objApi);

            dadosCarregado = true;
        }

    }
}
