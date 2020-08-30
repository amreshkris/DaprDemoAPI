# DaprDemoAPI
 Dapr - Asp.net core API with DAPR SDK

# DAPR

Dapr is  Distributed Application runtime - An open source, event driven , portable runtime that helps to build distributed system on cloud and on edge systems.

Visit DAPR Official Documentation at - [DAPR](https://dapr.io/)

# About this Repository

*Dapr Demo Api*

Dapr Demp Api - ASP.Net core Application to receive from the queue and save it into state store.

## Component Definition

- [Redis StateStore](/Deploy/redis-statestore.yaml)
- [Redis PubSub](/Deploy/redis-pubsub.yaml)
- [CosmosDB StateStore](/Deploy/cosmosdb.yaml)

Front End Code - That publishes into the *feedback* queue defined in this applicaiton is avaiable at  - [Dapr Demo APP](https://github.com/amreshkris/DaprDemoApp)

# Architecture Diagram

<img src="/Images/Demo_ArchitectureDiagram.png">

