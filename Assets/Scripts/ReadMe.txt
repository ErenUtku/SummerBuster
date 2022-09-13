13 Eylul:

Mini oyundan cikarimlarim:
-- Sahnede 3 adet stickman cesitli renklerde can simitlerini giymistir.
-- Amacimiz herhangi birini digerlerine can simitlerini dogru renkte vererek kurtarabilmek.
-- Diger 2 stickman ayni renk can simitlerini giymek zorundadir.
-- 

Mekanik
-- Can simitlerinin;
Tuttugumuzda doMoveY ile yukariya kalkiyor , biraktigimizda ise oyuncunun kafasinin ustundeki konuma tekrar gelip can simidi geri giriyor.
Can simidi DOGRU RENGI buldugunda o kisim partliyor ve birakildiginda oraya giriyor.


NASIL YAPABILIRIM; 
--En son eklenen can simidini tum oyunculara baz olarak alirim, ayni renkteyse eklenmesini saglarim.

Movement olarak; en son olagan cam simidini aktivesini ayarlayip uzerine basildiginda DOmoveY degerini oyuncu pozisyonu, birakildiginda tekrar oyuncu pozisyonunun ustunden
eski konumuna getiririm. Can simidini biraktigimda diger can simitlerininde degerlerinin degisiceginden dolayi SetColor diye bir event atarim, 
List olarak aldigim objelerin en sonuncusuna baktirip Setletirim.

Collider icin oyuncuyu kullanacagim, can simitlerinin colliderinin Z kismini yuksek tutup gormesini saglayacagim.


