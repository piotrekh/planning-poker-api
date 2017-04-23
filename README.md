# PlanningPoker.Api

Api for ['planning poker'](https://en.wikipedia.org/wiki/Planning_poker) application which allows users to collaboratively estimate tasks in software development.

## Quick start

1. Run **PlanningPoker.Database** project to setup the database (SQL Server required)
2. Run **PlanningPoker.TestData** project to insert test data
3. Run **PlanningPoker.Api** for the main application services
4. Use endpoint `api/token` with request fields `username` and `password` (x-www-form-urlencoded) to authenticate 
5. Call api methods with:
   
   - `Authorization` header and value `'Bearer [TOKEN]'` where [TOKEN] was obtained in step 4
   - `Content-Type` header and value `'application/json'`

