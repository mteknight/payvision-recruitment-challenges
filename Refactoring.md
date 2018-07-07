# Solution

A full refactoring was done throughout the projects in Refactoring folder. New unit tests were created to cover for the code that'd be refactored, service classes were created to segment the workflow into more meaningful scopes and factories were introduced to handle the creation of objects since there's no DI in place.

Although, there are scenarios that the factory couldn't be replaced (easily) by DI.

### Assumptions
1. There is no dependency injection;
2. Order class is an Aggregate/Entity and represents a Poco based on its properties;
3. FraudResult is a ValueObject/Dto;
4. Empty order lines are not a fraud, but could happen so are ignored;
5. Any empty, null or out of range order id field will be treated as exception.

### Remarks
1. Dependency injection should be a next step. All dependencies are now on interfaces, so this should be natural;
2. Subclassing should be used only on very specific cases. Order and FraudResult didn't seem to make sense to belong exclusively to FraudRadar;
3. Assumed the order email validation is as expected, so didn't go the extra mile to implement regex validation. It also involves business decision;
4. Implemented unit tests to parts that were changed through refactoring to ensure those were not broken and still do the same as original;
5. Tried different refactoring approaches to GetFrauds method, but in the end none of them presented substantial gain over the current implementation. It is already complex but can't go further IMHO;
6. Changed the filePath that was being sent as parameter to a FileStream that is only read when needed. Also handled some enconding issues;
7. For now, extension method ReadAllBytes can only read up to 2,147,483,647 bytes. It could be extended with extra logic to do a full read.
