
![tux](assets/tux.png)

## Install Dotnet on Ubuntu


wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh <br />
chmod +x ./dotnet-install.sh <br />
./dotnet-install.sh --version latest <br />
./dotnet-install.sh --version latest --runtime aspnetcore <br />
./dotnet-install.sh --channel 8.0 <br />



## Publisching on Ubuntu 
dotnet publish -c release  --self-contained -o ./bin/Publish


## Table `news'
<br />
CREATE TABLE <b>`news`</b> (<br />
&emsp; &ensp; `id` int(11) NOT NULL AUTO_INCREMENT,<br />
&emsp; &ensp; `provider` int(11) DEFAULT 1,<br />
&emsp; &ensp; `providerid` varchar(255) NOT NULL,<br />
&emsp; &ensp; `ts` datetime DEFAULT NULL,<br />
&emsp; &ensp; `lastupdated` datetime DEFAULT NULL,<br />
&emsp; &ensp; `productcodes` varchar(1024) DEFAULT NULL,<br />
&emsp; &ensp; `headline` varchar(1024) DEFAULT NULL,<br />
&emsp; &ensp; `body` text DEFAULT NULL,<br />
&emsp; &ensp; `keywords` varchar(1024) DEFAULT NULL,<br />
&emsp; &ensp; `source` varchar(1024) DEFAULT NULL,<br />
&emsp; &ensp; `priority` decimal(2,0) DEFAULT NULL,<br />
&emsp; &ensp; `country` char(2) DEFAULT NULL,<br />
&emsp;   PRIMARY KEY (`id`)<br />
&emsp; ) ENGINE=InnoDB ; <br />