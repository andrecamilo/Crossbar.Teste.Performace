using System;
using Crossbar.Teste.Calculo1.DTOs;
using System.Threading;

namespace Crossbar.Teste.Calculo1.Domain
{
    public class Calcular
    {
        public CalculoCalc1Retorno Run(CalculoCalc1Parametro param)
        {
            Thread.Sleep(param.TempoEspera);

            var obj = new CalculoCalc1Retorno
            {
                TempoExecucao = param.TempoEspera,
                DataInicio = param.DataInicio,
                NumeroChamada = param.NumeroChamada
            };

            Console.WriteLine(string.Format("Executou calc 1, inicio: {0}, Numero chamada: {1}", obj.DataInicio, param.NumeroChamada));

            return obj;
        }
    }
}
