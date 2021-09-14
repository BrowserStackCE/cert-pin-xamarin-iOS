if grep -q "my.machine" <<< $(cat /etc/hosts);
	then exit 0;
fi;
sudo echo "127.0.0.1 my.machine" >> /etc/hosts;
