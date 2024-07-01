using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Npgsql.PostgresTypes;

namespace BlockChainTracer.Model;
public class CrossChainSwap
{
    public long Id { get; set; }
    public IsolatedTransaction InputTransaction { get; set; }
    public IsolatedTransaction OutputTransaction { get; set; }
    public string BridgeName { get; set; }
    public DateTime DateTime { get; set; }

    public CrossChainSwap(IsolatedTransaction inputTransaction, IsolatedTransaction outputTransaction, string bridgeName, DateTime dateTime)
    {
        InputTransaction = inputTransaction;
        OutputTransaction = outputTransaction;
        BridgeName = bridgeName;
        DateTime = dateTime;
    }

    public CrossChainSwap(IsolatedTransaction inputTransaction, IsolatedTransaction outputTransaction, DateTime dateTime)
    {
        InputTransaction = inputTransaction;
        OutputTransaction = outputTransaction;
        DateTime = dateTime;
    }

    public CrossChainSwap() { }

}
