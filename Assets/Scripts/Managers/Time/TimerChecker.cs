using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerChecker : MonoBehaviour
{
    public Button Entrada;
    public Button Salida;

    public string fecha;
    public TimeSpan hora;
    public int dia;
    public int mes;

    private string fechaSalida;
    private TimeSpan horaSalida;
    private string horaSalidaString;
    public int diaSalida;
    private int mesSalida;

    private TimeSpan TiempoAfk;
    public TimeSpan CurrentTime;
    public string[] partesFecha;


    void Start()
    {

        //CurrentTime = TimeSpan.Parse(TimeManager.sharedInstance.getCurrentTimeNow());

        StartCoroutine(checkTime());
    }
    void LateUpdate()
    {


        /*  if (Input.GetMouseButtonDown(0)) //entra
          {
              //StartCoroutine(checkTime());
              //Check();
             // print("la longitud del array aumenta ? " + partesFecha.Length);
          }
          if (Input.GetMouseButtonDown(1))  //sale
          {
             // StartCoroutine(checkTime());

          }
             */

    }

    public IEnumerator checkTime()
    {
        yield return StartCoroutine(TimeManager.sharedInstance.getTime());
        updateTime();
        updateDate();
    }

    void updateTime()
    {
        hora = TimeSpan.Parse(TimeManager.sharedInstance.getCurrentTimeNow());
    }
    void updateDate()
    {
        fecha = TimeManager.sharedInstance.getCurrentDateNow();
        partesFecha = fecha.Split('-');
        mes = int.Parse(partesFecha[0]);
        dia = int.Parse(partesFecha[1]);
    }


    void Check()
    {
        if (fechaSalida == "")
        {
            Debug.Log("Es su primerito dia");

        }
        else
        {
            mesSalida = PlayerPrefs.GetInt("mesSalida", mesSalida);
            diaSalida = PlayerPrefs.GetInt("diaSalida", diaSalida);
            horaSalidaString = PlayerPrefs.GetString("horaSalidaString", horaSalidaString);
            horaSalida = TimeSpan.Parse(horaSalidaString);

            ComprobarMes();
        }

    }

    public void LogIn()
    {
        print("Entro");
        fechaSalida = PlayerPrefs.GetString("fechaSalida", fechaSalida);
        print(fechaSalida);
        StartCoroutine(checkTime());
        Check();

    }

    public void LogOut()
    {
        // print("Salgo");
        StartCoroutine(checkTime());

        fechaSalida = fecha;
        mesSalida = mes;
        diaSalida = dia;
        horaSalida = hora;
        horaSalidaString = horaSalida.ToString();
        PlayerPrefs.SetString("fechaSalida", fechaSalida);
        PlayerPrefs.SetString("horaSalidaString", horaSalidaString);
        PlayerPrefs.SetInt("diaSalida", diaSalida);
        PlayerPrefs.SetInt("mesSalida", mesSalida);

    }




    void ComprobarMes()
    {
        if (mes == mesSalida)
        {
            ComprobarDia();
        }
        else if (mes != mesSalida)
        {
            if ((dia == 1 && (diaSalida == 31 || diaSalida == 30 || diaSalida == 29 || diaSalida == 28)) && mes == (1 + mesSalida))
            {

                hora += new TimeSpan(24, 00, 00);
                TiempoAfk = hora - horaSalida;
                print("tiempo" + TiempoAfk);
            }
            else
            {
                //recompensas max
            }
        }
    }


    void ComprobarDia()
    {

        if (dia != diaSalida)
        {
            hora += new TimeSpan(24, 00, 00);
            TiempoAfk = hora - horaSalida;
            print("tiempo" + TiempoAfk);

        }
        else if (dia == diaSalida)
        {
            print("Hola buenos dias");
            TiempoAfk = hora - horaSalida;
            print("tiempo" + TiempoAfk);
        }
    }
    /*  private IEnumerator CheckTime()
    {
     Debug.Log("==> Checking the time");
     timeLabel.text = "Checking the time";
     yield return StartCoroutine(
         TimeManager.sharedInstance.getTime()
     );
     updateTime();
     Debug.Log("==> Time check complete!");
 }
     * 
     * 
     * TimeSpan interval1, interval2;
     interval1 = new TimeSpan(1, 45, 16);
     interval2 = new TimeSpan(1,18, 12, 38);
    print("{0:G} - {1:G} = {2:G} " + (interval1 - interval2));
    print(String.Format(new CultureInfo("fr-FR"), "{0:G} + {1:G} = {2:G}", interval1, interval2, interval1 + interval2));
     interval1 = new TimeSpan(0, 0, 1, 14, 36);
     interval2 = TimeSpan.FromTicks(2143756);
     print("{0:G} + {1:G} = {2:G}"+ interval1 + " " + interval2 + " " + (interval1 + interval2));
     */

}