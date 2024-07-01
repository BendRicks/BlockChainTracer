# CrossChainSwapTracer

## General info
Project for tracking cross-chain swaps between EVM based networks. 

## Technologies
Project is created with C#. Database is PostgreSQL.

## Setup
To run this project, download [latest release](https://github.com/BendRicks/CrossChainSwapTracer/releases/latest), than unpack it.

Firstly, you need to examine settings.json file. There you can find the following options:
* useDatabase - defines if app need to use database to store cross-chain swaps
* dbParams - defines params that will be used to connect the database
* bridges - list of supported bridges
  * logProcessorClassName - the classname of log processor class (class must implement ILogProcessor interface)
* chains - list of EVM based chains to trace
  * url - url of chain node
  * supportedBridges - list of bridges names that are working in that chain
  * period - new blocks request period in milliseconds
  * startBlock - start block number, from which processing will begin
  * endBlock - end block number, on which processing will stop

If both startBlock and endBlock are set to 0, app will start processing blocks in realtime. If only endBlock is set to 0, app will process blocks from startBlock to the last available block in chain.
 
After configuring settings.json, you need to start CrossChainSwapTracer.exe file.
