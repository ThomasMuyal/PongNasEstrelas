# PongNasEstrelas
Repositório com código e arquivos correspondentes ao projeto de Unity para a matéria da Poli-USP PSI3502-2019 Realidade Virtual
Exportação do projet ofeita no seguinte formato:
asd
  -arquivo .unitypackage
  -pasta com assets
Para importação do ambiente para teste e uso do jogo, seguir o seguinte protocolo
1. Baixar os arquivos UnityExportPackage.unitypackage e a pasta Assets do repositório 
2. Abrir novo projeto 3D em Unity (versão utilizada 2018.4.11f1 Personal, DX11) 
3. Menu Edit > Import Package > Custom Package e selecionar o arquivo UnityExportPackage.unitypackage
4. Importar assets para área de assets do novo projeto criado

Observa-se que é necessário um ambiente com os plug-ins utilizados já configurado para abertura do projeto desta maneira. Foram utilizados os plug-ins Machine Learning Agents na versão 0.11.0, não incluido na versão base baixável do Unity. Este plug-in requer os pacotes
Tensorflow v1.14.0
Tensorboard v1.14.0
Python v3.6.8
Keras-Applications 1.0.8
Keras-Preprocessing 1.1.0
jupyter 1.0.0

Para execução do jogo, baixar a pasta Build do repositório e executar Pongue v3_ia.exe
Observa-se que não é necessária a instalação dos plug-ins para execução deste arquivo executável.
