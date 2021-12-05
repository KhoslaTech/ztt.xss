The Zero-Trust Thinking series: Cross-Site Scripting (XSS) Samples
--------------------
This repository contains samples that go in depth into [Cross-Site Scripting](https://aspsecuritykit.net/guides/protecting-your-users-against-cross-site-scripting-xss-attacks/) (XSS) as part of the Zero-Trust Thinking series, brought to you by the [ASPSecurityKit team](https://ASPSecurityKit.net/).

The repo has multiple projects each of which is self-contained, and also has an associated video on the [ASPSecurityKit](https://www.youtube.com/channel/UCuFQfuCzIg0U-Aa6dk7JzzQ) Youtube channel. With These samples and videos, we let you orchestrate different kinds of XSS attacks and help you build the Zero-Trust mindset to definitively defend against XSS.

The code in each of the projects contains implementation of a simple Classifieds website where users(buyers/sellers) can sign up and add the products they want to sell on the platform. Similarly signed up users can view the products on display. We would try to simulate the attack scenarios through two users namely the attacker and the victim. Hence you would have to create two users after running the project.

### ZTT.XSS.Attack
The code sample in this project simulates [three kinds of XSS attacks](https://aspsecuritykit.net/guides/protecting-your-users-against-cross-site-scripting-xss-attacks/#types-of-xss):

- Stored XSS: To simulate this attack, login as the attacker and then try adding a product. While adding, enter the description field of the product as below, which is a script that is going to be persisted in the Classifieds database when the product record is saved.

	```js
	<script>alert(document.cookie.substr(document.cookie.indexOf('AspNetCore.Identity.Application')));</script>
	```

	Next, login as the victim user and browse to the `Products` listing page and then view the details, which includes the description of the product added by the attacker, the script injected earlier as part of the description field is going to be executed on the browser thereby immediately revealing the victim's session token to the attacker.

- Reflected XSS: For this attack we are going to simulate it via a link which does not exist in the app such as below and hence results in a `404`

	```bash
	https://localhost:[port]/Products1<script>alert(document.cookie.substr(document.cookie.indexOf('AspNetCore.Identity.Application')));</script>
	```

	As you'll see when the victim logs in and then clicks this link which points to an invalid endpoint `/Producs1` it results in a `404`. The page displaying the `404` invalid url message causes the injected script to execute and thereby the session token is immediately captured by the attacker.

- DOM Based XSS: This attack happens on the client side. We simulate it by manipulating a feature of adding discount code to a product via the query string in the URL.

	```bash
	https://localhost:[port]/Products/Details/c7aeecbe-d4e9-4a7e-849c-aa9fdf27755d?discountCode=magic42<script>alert(document.cookie.substr(document.cookie.indexOf(%27AspNetCore.Identity.Application%27)));</script>
	```

	The victim may login and click the above link causing the script to be run and thereby causing his session token to be compromised. 

Running the sample
--------------------

### Prerequisites
* [Visual Studio](https://visualstudio.microsoft.com/) 2019 or higher

### Steps:
* First, clone this repo or download it as zip file.
* Open ZTT.XSS.sln in Visual Studio 2019 or higher.
* In Package Manager Console, execute the following command to create the database for either ZTT.XSS.Attack or ZTT.XSS.Prevention:
	```ps1
	update-database
	```
* Press F5 to run in debug mode.    

Feedback
--------------------

Feedback is much appreciated. For any question, to report any issue or need some help, feel free to get in touch on [support@ASPSecurityKit.net](mailto:support@ASPSecurityKit.net)

License
--------------------

This sample source code is licensed under [KHOSLA TECH - END USER AGREEMENT](https://aspsecuritykit.net/legal/end-user-agreement/)