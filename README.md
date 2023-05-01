
This project created for coding test.

 - The web api end point accepts POST method with a request body.  
    ```
    {
        "emailMessage": ""
    }
    ```

 - Implemeted versioning for the controller  
    >http://localhost:5028/api/v1/expense/createexpense  

 - Response for invalid request is 500 internal server error with message  
    >'The request is invalid, please contact administrator.'
- Implemented Swagger
    >http://localhost:5028/swagger/index.html
- Implemented Serilog logging with file Sink option  
    >File path - outside project folder ../logs/log.txt
- Created unit test project using XUnit
    >Faced issue while running test and solved by adding the following packages
    >Microsoft.NET.Test.Sdk  
    >xunit.runner.visualstudio
- Input
    - Valid scenario
        ```
        {
        "emailMessage": "Hi Patricia,Please create an expense claim for the below.  Relevant details are marked up as requested...<expense><cost_centre>DEV632</cost_centre><total>35,000</total><payment_method>personalcard</payment_method></expense>From: William SteeleSent: Friday, 16 June 2022 10:32 AMTo: Maria WashingtonSubject: testHi Maria,Please create a reservation for 10 at the <vendor>Seaside Steakhouse</vendor> for our<description>development team’s project endcelebration</description> on <date>27 April2022</date> at 7.30pm.Regards,William"
        }
        ```

   - Invalid - const centre UNKNOWN
       ```
        {
        "emailMessage": "Hi Patricia,Please create an expense claim for the below. Relevant details are marked up as requested...<expense><cost_cente>DEV632</cost_cente><total>35,000</total><payment_method>personalcard</payment_method></expense>From: William SteeleSent: Friday, 16 June 2022 10:32 AMTo: Maria WashingtonSubject: testHi Maria,Please create a reservation for 10 at the <vendor>Seaside Steakhouse</vendor> for our<description>development team’s project endcelebration</description> on <date>27 April2022</date> at 7.30pm.Regards,William"
        }
        ```

    - Invalid - total missing
        ```
        {
        "emailMessage": "Hi Patricia,Please create an expense claim for the below. Relevant details are marked up as requested...<expense><cost_cente>DEV632</cost_cente><payment_method>personalcard</payment_method></expense>From: William SteeleSent: Friday, 16 June 2022 10:32 AMTo: Maria WashingtonSubject: testHi Maria,Please create a reservation for 10 at the <vendor>Seaside Steakhouse</vendor> for our<description>development team’s project endcelebration</description> on <date>27 April2022</date> at 7.30pm.Regards,William"
        }
        ```


 - References:
     - Serilog logging  
     https://www.claudiobernasconi.ch/2022/01/28/how-to-use-serilog-in-asp-net-core-web-api/

     - Deserialising the total value  
     https://stackoverflow.com/questions/73451496/c-sharp-xmlserializer-deserialize-number-with-number-group-separator

     - XUnit not run   
     https://stackoverflow.com/questions/47894776/xunit-unit-tests-will-not-run


