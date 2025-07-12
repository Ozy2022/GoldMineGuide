### 🪙 **Gold Mine Guide**

This is an **ASP.NET MVC web application** hosted on **AWS**, built for the **mining industry** to improve communication, project tracking, and data sharing between staff and management, especially post-COVID.

### 🎯 Main Objectives:

* Provide a **user-friendly web platform** for the mining sector.
* Save time compared to traditional communication/reporting methods.
* Enable **real-time project tracking** and updates.
* Use **AWS X-Ray** for performance monitoring.
* Allow **dynamic scalability** to handle peak usage.

---

### 👥 **User Roles**:

#### 🧑‍💼 Manager (Admin):

* Logs in to manage and view miner/staff accounts.
* Can receive **confirmation messages** from staff.
* Uses **AWS SNS** to communicate effectively.

#### ⛏️ Staff (Miners):

* Can create, edit, and delete **gold mining records**.
* Manage personal profiles.
* View site updates and post reports.
* Admins assign miners to sites, but staff have full control over site detail entries.

---

### 📷 **Screens & Features**:

**Part 1 – General Pages:**

* **Home**
* **About Us**
* **Sign Up / Login**
* **Gallery**
* **Privacy & Policy**
* **Services**

**Part 2 – Mining Functionalities:**

* View mining sites and info.
* Send/receive confirmation messages.
* View and manage user profiles.

---

### ☁️ **Cloud Features & AWS Tools**:

* **AWS SNS**: Send notifications (email, SMS, push) to users.
* **AWS Lambda**: Handle backend logic with scalability and security.
* **Amazon CloudWatch & X-Ray**:

  * Track and analyze performance.
  * Monitor latency and success rates.
  * Example result: 86% request success rate shown in service map.

---

### 🏗️ Cloud Architecture:

The system uses a **serverless and scalable design**, relying heavily on AWS services to:

* Reduce manual infrastructure management.
* Improve communication.
* Enhance user experience during peak sessions.

---

#Project_Screenshots:

#Part1

1-HomePage:

![Home](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/89e1ab66-23f4-4c54-b396-447224ddae83)

2-AboutUS

![About](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/9f56e565-e16e-48f1-9a23-5563f8be6aa7)

3-Sign up and Sign in page

![Sign-up](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/50d9a220-b76f-4d7b-afd2-43504a468c6b)

![login](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/27c61175-70c1-44c1-8c60-b58d972965d2)


4-GalleryPage

![Gallery](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/4be086e2-9bc8-4488-aa57-029f3cb0614d)

5-Privacy&Policy

![Capture](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/e94639d8-04cd-467e-8651-a75981ce1785)

6-Services

![services](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/6a483709-7ead-4531-bebe-bcd71673099c)

------------------------------------------------------------------------------------------------------------------------

Part2 

1-Mining_Gold_List

![GoldMineInfoTable](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/f4b8cd2f-0ad3-4a45-801e-e0cd95087fb1)

2-Confirmation_Message

![Confirmation_message](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/1247645c-aa39-45e3-b1a1-a9d87d1f53ed)

3-User_Profile

![UserProfile](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/f5d87b20-3b57-481d-ad7f-eb2663cccfa4)


------------------------------------------------------------------------------------------------------------------------

##AWS_Services

Amazon SQS, AWS Lambda, HTTP, email, mobile push notifications, and mobile text messaging (SMS) are all available for subscribers to use to receive published messages.


##Cloud_Architecture_Style


![Captures](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/d2e81f31-20b0-4ebe-ba7f-7a94c4b870f8)


1-AWS_Lambda

Using AWS Lambda, you may build your own backend services that have the same scalability, speed, and security as the restof AWS's offerings, or you can add custom logic to existing AWS services.


![Lambda](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/7e839557-8eb8-4282-8202-691af15306d2)


2-SNS_Service


![SNS_Confirm](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/f9ec7d46-9015-4250-aa9b-7ec8dd5c47a5)


![emailmessage](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/b3c0e3f4-c6bd-431b-826b-73c386826d33)



3-CloudWatch 

X-Ray is a useful tool that was uncovered throughout the course of this research. It is applied to websites in order to improve their load times and determine how much latency they experience.


![ServiceMap](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/3b880024-db59-4875-83b5-e569959a1bc0)


![ARN1_Trace](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/72657a5e-06da-4ee4-8fbc-67ee4565822d)


![1](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/2df1e0c3-0939-4a90-9db9-b69a83ca9d34)


![Capture](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/51544647-b8bf-451d-b3d5-8bc9e6cd1a10)



![ResourcofARN](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/5dceb0a2-f7a6-4c9a-80dd-b9349ba73610)



![websitLink](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/5bd9978d-8125-4eba-abe6-259116b98e81)



3-Amazon_X-Ray

AWS X-Ray provides full end-to-end visibility by tracking requests to apps that
span various AWS accounts, AWS Regions, and Availability Zones. However, the
services map as it shown in the fig is works and the success percentage of whole
web application is basically in in the fig below shows 86% ok.


![Capture](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/1d5368c7-af97-43c5-a459-bc2cb3d0bb80)


![2](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/851bb597-2d07-4ae3-9bf2-6ab3051f9ba8)


![Capture](https://github.com/Ozy2022/GoldMineGuide/assets/96604157/2ff8e1d0-6b85-4848-bc29-713d19a5f360)









