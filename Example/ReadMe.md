# Pub-Sub Actor Example



|    publisher  |     broker    |subscriber|
| ------------- |:-------------:| -----:|
|[Publishing Actor]||
||[BrokerService]|
|||[Subscribing Actors]|
|||[Subscribing Services]|
|[Publishing Service]||
||[BrokerService]|
|||[Subscribing Services]|
|||[Subscribing Actors]|

Or if you like using Actors, you can use the BrokerActor:

|    publisher  |     broker    |subscriber|
| ------------- |:-------------:| -----:|
|[Publishing Actor]||
||[BrokerActor]|
|||[Subscribing Actors]|
|||[Subscribing Services]|
|[Publishing Service]||
||[BrokerActor]|
|||[Subscribing Services]|
|||[Subscribing Actors]|

For large scale messaging with many subscribers you can use a layered approach using RelayBrokerActors:

|    publisher  |     broker    | relaybrokers | subscriber|
| ------------- |:-------------:|:-------------:| -----:|
|[Publishing Actor]||
||[BrokerActor]|
|||[RelayBrokerActors]|
||||[Subscribing Actors]|
||||[Subscribing Services]|
|[Publishing Service]||
||[BrokerActor]|
|||[RelayBrokerActors]|
||||[Subscribing Services]|
||||[Subscribing Actors]|
*note: only message subscriptions are different here, publishing still happens using the default broker*


S1----->Table1(Case1)
S2----->Table1(Case2)
S3----->Table1(Case3)
S4----->Table1(Case4)


S5----->Table2(Case1)
S6----->Table2(Case2)
S7----->Table2(Case3)
S8----->Table2(Case4)


S6----->Table3(Case1)
S7----->Table3(Case2)
S8----->Table3(Case3)
S9----->Table3(Case4)






  