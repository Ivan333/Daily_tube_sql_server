VizuelnoProekt 
============== 
Целта на играта е да се соборат вселенските бродови на марсовците, и да се освојат што е можно повеќе поени во трите нивоа на играта. 
Постои и база, во која се чуваат поените од играчите. 
 
Класи во проектот 
============== 
Проектот се состои од следниве класи/фолдери: 
 
Attack фолдерот во кој се наоѓаат attack класите  
- SpaceShip classes во кој се наоѓаат класите EnemySpaceShip.cs, PlayerSpaceShip.cs,SpaceShip.cs 
- Controller.cs класата 
- Home.cs за прикажување на главното мени 
- ScoreInput форма за прокажување на поените кој ги освоил играчот 
- Form3 форма за прикажување на правилата на игра 
- HighScores форма за прикажување на поените од сите играчи зачувани во базата 
- Resourses1.resx овде се чуваат песните 
- UserScore.mdf базата во која се чуваат поените од играчитеHighScores форма HighScores формата 
- UserScoreDataSet.xsd dataset за прикажување на податоците од базата во  
- Game.cs Овде се исцртува играта, тука е и тајмерот и евентите за регистрирање на копчињата на тастатура.


За овие класи можете да прочитате подетална документација во самиот код 
 
 
 
Controller.cs класата 
============== 
Во оваа класа се случуваат најважните работи во играта. 
Овде се чуваат бродовите на непријателите и на играчот (EnemySpaceShip.cs i PlayerSpaceShip.cs), нападите како и методите за придвижувањето на сите објекти на екранот.
Бродот на играчот, непријателите и нападите се чуваат во листи.
 
 
Како се игра 
============== 
Играта е базирана на познатата аркадна видое игра Space Invaders. Играва се содржи од три нивоа секое со разлицна тезина. 
Целта е да се уништат непријателските вселенски бродови, а со тоа и да се освојат што повеке поени. 
Контроли: 
Спејс е за пукање, левац и десна стрелка од тастатурата служат за движење лево или десно

Слики
==============
![alt tag](http://oi61.tinypic.com/qq2zyt.jpg)



