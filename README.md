# Electronic Medical Record - Dynamic Form Solution
Electronic Medical Records for Viet Duc hospital. Reduce time when for old patient's records lookup, hence improve the ability to continuous examination and treatment 

Notation Technology Ideas: 
- Dynamic Form (based on idea of designing eCommerce's product portfolio aka [EAV-model](https://en.wikipedia.org/wiki/Entity%E2%80%93attribute%E2%80%93value_model))
- CQRS Model - Provide performance when seperate application into 2 seperate read/write models, since storing dyamic form inside RDBMS using EAV can only leverage ACID compliant, NoSQL Database (in this application, it's MongoDB) can improve the speed of read when recursive structure stored within RDBMS is converted into NoSQL structure.
