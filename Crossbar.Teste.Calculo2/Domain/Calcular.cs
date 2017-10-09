using System;
using Crossbar.Teste.Calculo2.DTOs;
using System.Threading;

namespace Crossbar.Teste.Calculo2.Domain
{
    public class Calcular
    {
        public CalculoCalc2Retorno Run(CalculoCalc2Parametro param)
        {
    
            Thread.Sleep(param.TempoEspera);

            var obj = new CalculoCalc2Retorno
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
