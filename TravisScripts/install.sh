# This link changes from time to time.
echo 'Downloading from https://download.unity3d.com/download_unity/ac7086b8d112/MacEditorInstaller/Unity-5.6.4f1.pkg: '
curl -o Unity.pkg https://download.unity3d.com/download_unity/ac7086b8d112/MacEditorInstaller/Unity-5.6.4f1.pkg
echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /
