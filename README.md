Exercise 1.

Description: Inside the solution you will find implementation of the first Exercise (1.Supermarket). You can control the initial parameters for the application from app.config:
OrderProcessingMinTimeInSec – Minimal order processing time of each cashier (numeric)
OrderProcessingMaxTimeInSec - Maximal order processing time of each cashier (numeric)
NumberOfCashiers - Queue of multiple cashiers In the supermarket (numeric)
PeopleJoinQueueRatePerSecond - People join the queue rate (numeric)

It is an implementation of Publisher-Subscriber design pattern.  Cashier.cs is a worker thread that is notifying QueueManager as soon as it finished processing current customer. The QueueManager.cs is handling the customers queue and assign new customer to the most available Cashier.
Add Time & Space complexity:
The application has 2 queues (O(n) complexity). It provides special priority to the customers that came first (FIFO). It also consumes most available Cashiers – meaning the first available Cashier will be handling customers.


Exercise 2.

Description: 2.Exercise contains implementation for the second assignment. You can use the “CreateFile” method to generate text file for tests. It gets user input string and generates ALL possible permutations for the given string. For example if input is ABCDD, the application will generate DCDAB, CDDAB and etc. Then it will store results hash code inside HashSet. The hashset will be used as a lookup table. As a result the search complexity is ~O(1).
How-To: once you have generated test text file that has lines of 5 characters strings, set file’s path to the “filePath” variable. Now the application can be executed!

Do not hesitate to ask me anything about the given solutions.

Sincerely,
David L.
