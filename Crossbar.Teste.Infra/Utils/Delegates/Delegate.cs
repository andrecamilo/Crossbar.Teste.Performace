namespace Crossbar.Teste.Infra.Utils.Delegates
{
    public delegate TRetorno ExecutarCallee<TRetorno, TParametro>(TParametro parametros);

    public delegate void ResultCaller<TRetorno>(TRetorno resultados);

    public delegate void GetMessage<TMensagem>(TMensagem value);
}