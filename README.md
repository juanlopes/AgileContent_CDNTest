# Backend Test

## Problem
Log ﬁles can tell much about a system’s behavior in a production environment.
Extracting data from these ﬁles helps the decision-making process for both business
and development roadmap.

iTaaS Solution is a company focused on content delivery, and one of its largest business
challenges was CDN (Content Delivery Network) costs. Larger CDN costs increase the
ﬁnal pricing for its customers, reduce proﬁts, and make it diﬃcult to enter smaller
markets.

After extensive research from their software engineers and ﬁnancial team, they
obtained an excellent deal from the company “MINHA CDN”, and signed a one-year
contract with them.

iTaaS Solution already has a system capable of generating billing reports from logs,
called “Agora”. However, it uses a speciﬁc log format, diﬀerent from the format used by
”MINHA CDN”.

You have been hired by iTaaS Solution to develop a system that can convert log ﬁles to
the desired format, which means that at this moment they need to convert them from
the “MINHA CDN” format to the “Agora” format.

## Examples

This is a sample log ﬁle in the “MINHA CDN” format:

```
312|200|HIT|"GET /robots.txt HTTP/1.1"|100.2
101|200|MISS|"POST /myImages HTTP/1.1"|319.4
199|404|MISS|"GET /not-found HTTP/1.1"|142.9
312|200|INVALIDATE|"GET /robots.txt HTTP/1.1"|245.1
```

The sample above should generate the following log in the “Agora”format:
```
#Version: 1.0

#Date: 15/12/2017 23:01:06

#Fields: provider http-method status-code uri-path time-taken

response-size cache-status

"MINHA CDN" GET 200 /robots.txt 100 312 HIT

"MINHA CDN" POST 200 /myImages 319 101 MISS

"MINHA CDN" GET 404 /not-found 143 199 MISS
"MINHA CDN" GET 200 /robots.txt 245 312 REFRESH\_HIT
```

### Acceptace Criteria

“MINHA CDN” will make log ﬁles through speciﬁc URLs.
The speciﬁcation requires you to implement a Console Application that receives as input
an URL (sourceUrl) and a destination ﬁle (targetPath). A sample call would be:

```
convert http://logstorage.com/minhaCdn1.txt ./output/minhaCdn1.txt
```

The sourceUrl parameter includes a ﬁle in the “MINHA CDN” format, and the output
speciﬁed in the targetPath parameter corresponds to the ﬁle that needs to be created in
the “Agora” format.

All namespaces should follow the pattern:

CandidateTesting.YourFullName.YourNamespace

A sample log ﬁle that can be used for testing is available here:

https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt

Please be aware that what will be analyzed in this exercise is not only the correction of
the code but also coding best practices, like OOP, SOLID, unit tests and mocks.
