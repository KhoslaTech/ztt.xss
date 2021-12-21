The Zero-Trust Thinking series: Cross-Site Scripting (XSS) Samples
--------------------
This repository contains samples that go in depth into [Cross-Site Scripting](https://aspsecuritykit.net/guides/protecting-your-users-against-cross-site-scripting-xss-attacks/) (XSS) as part of the Zero-Trust Thinking series, brought to you by the [ASPSecurityKit team](https://ASPSecurityKit.net/).

The repo has multiple projects each of which is self-contained, and also has an associated video on the [ASPSecurityKit](https://www.youtube.com/channel/UCuFQfuCzIg0U-Aa6dk7JzzQ) Youtube channel and a step-by-step walkthrough on the [Zero Trust Thinking series](https://ASPSecurityKit.net/blog/zero-trust-thinking-ztt/#articles-and-episodes) page. With These samples and videos, we let you orchestrate different kinds of XSS attacks and help you build the Zero-Trust mindset to definitively defend against XSS.

The code in each of the projects contains implementation of a simple Classifieds website where users(buyers/sellers) can sign up and add the products they want to sell on the platform. Similarly signed up users can view the products on display. We would try to simulate the attack scenarios through two users namely the attacker and the victim. Hence you would have to create two users after running the project.

### ZTT.XSS.Attack
The code sample in this project simulates [three kinds of XSS attacks](https://aspsecuritykit.net/guides/protecting-your-users-against-cross-site-scripting-xss-attacks/#types-of-xss):

- Stored XSS: To simulate this attack, you can use the following XSS code which you need to put in the product description field. For step-by-step instructions, check out [this article](https://ASPSecurityKit.net/blog/understand-cross-site-scripting-xss-by-examples-zero-trust-thinking/#stored-xss).

	```js
	<script>alert(document.cookie.substr(document.cookie.indexOf('AspNetCore.Identity.Application')));</script>
	```

- Reflected XSS: For this attack we are going to simulate it via a link which does not exist in the app such as below and hence results in a `404`. For step-by-step instructions, check out [this article](https://ASPSecurityKit.net/blog/understand-cross-site-scripting-xss-by-examples-zero-trust-thinking/#reflected-xss).

	```bash
	https://localhost:[port]/Products1<script>alert(document.cookie.substr(document.cookie.indexOf('AspNetCore.Identity.Application')));</script>
	```

- DOM Based XSS: This attack happens on the client side. We simulate it by manipulating a feature of adding discount code to a product via the query string in the URL such as given below. For step-by-step instructions, check out [this article](https://ASPSecurityKit.net/blog/understand-cross-site-scripting-xss-by-examples-zero-trust-thinking/#dom-based-xss).

	```bash
	https://localhost:[port]/Products/Details/c7aeecbe-d4e9-4a7e-849c-aa9fdf27755d?discountCode=magic42<script>alert(document.cookie.substr(document.cookie.indexOf(%27AspNetCore.Identity.Application%27)));</script>
	```

### ZTT.XSS.Prevention.BasicDetection
The code sample in this project has a RegularExpressionValidator based detection to prevent XSS injection via input. 

To test it in action, you can use the following XSS code which you need to put in the product description field. For step-by-step instructions, check out [this article](https://aspsecuritykit.net/blog/learn-about-defending-against-cross-site-scripting-xss-input-injection-by-examples-zero-trust-thinking/#detect-input-injection).
```js
<script>alert(document.cookie.substr(document.cookie.indexOf('AspNetCore.Identity.Application')));</script>
```

### ZTT.XSS.Prevention.Full
The code sample in this project has a complete solution (except CSP) based on Zero Trust approach to defend against XSS in both input and output. 

To test it in action, you can use the following XSS code snippets:
- Stored XSS input injection (step-by-step instructions to try it out is available on [this article](https://aspsecuritykit.net/blog/learn-about-defending-against-cross-site-scripting-xss-input-injection-by-examples-zero-trust-thinking/#scale-it-with-aspsecuritykit).
```js
<script>alert(document.cookie.substr(document.cookie.indexOf('AskAuth')));</script>
```

- Reflected XSS input injection (step-by-step instructions to try it out is available on [this article](https://aspsecuritykit.net/blog/learn-about-defending-against-cross-site-scripting-xss-input-injection-by-examples-zero-trust-thinking/#check-out-with-a-new-addition).
```bash
	https://localhost:[port]/Product/?term=iPhone<script>alert(document.cookie.substr(document.cookie.indexOf(%27AskAuth%27)));</script>
```

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