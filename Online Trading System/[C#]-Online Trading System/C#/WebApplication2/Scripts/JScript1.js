$(document).ready(function () {

    $(".bigheader").animate({ left: '700px' }, 1500);
    $(".bigheader").animate({ left: '110px' }, 1500);


    var y = 0;
    var z;
    var pic;
    var pan;
    var ifr;
    var che;
    var pri;
    var lab;
    var labid;
    var isHidden;
    var but;

    var img;
    var fa;
    var Mv;
    var mv;
    var siburl;

    var Pnum;

    var info;
    //button event
    $(":submit").dblclick(function () {

        sib = $(this).parent().siblings().fadeToggle();

        num = $(this).attr('id');
        Pnum = (num[num.length - 1]);

        ishidden = sib.is(':hidden');

        setTimeout(function () {
            //wait untill all the sibling gone 
            ishidden = sib.is(':hidden');
            if (ishidden == true) {
                //



                var div = document.createElement("div");

                 
                div.className = "Div";
              
             
              

                //
                  $(".Div").append("<li>Product Name:", ProductName[Pnum],
             "<li>Date Update:", ProductDateUpdate[Pnum],
             "<li>Category:", ProductCategory[Pnum],
             "<li>Discript:", ProductDiscript[Pnum],
             "<li>Product count:", ProductCount[Pnum],
             "<li>Instock:", ProductInstock[Pnum],
             "<li>Price:", ProductPrice[Pnum]).attr('id');



            }

            else if (ishidden == false) {


                $(".Div").remove();


            }

        }, 5000);



    })



    //Panel event
    $(".front").dblclick(function () {



        pan = (this).id;
        pic = $(this).find('img').attr('id');
        url = $(this).find('img').attr('class');
        ifr = $(this).find('iframe').attr('id');
        che = $(this).find(":checkbox").attr('id');
        but = $(this).find("submit").attr('id');

        lab = (this).childNodes[1].id;
        pri = (this).childNodes[3].id;
        priceid = "#" + pri.toString();
        cheid = "#" + che.toString();
        labid = "#" + lab.toString();


        //        sibling var



        //

        if (ifr == null) {




            myMovie(url, pan);

            $("#trailer" + pan.toString()).hide();



        }

        isHidden = $("#trailer" + pan.toString()).is(':hidden')

        if (isHidden == true) {

            y = 0;


        }
        else if (isHidden == false) {

            y = 1;

        }


        y = y + 1;
        z = y % 2;




        $(this).toggleClass('Tfront');



        if (z == 1) {


            $(this).children().fadeOut(900);

            $(labid).toggleClass('RLabel').fadeIn(300);
            $("#trailer" + pan.toString()).delay(1000).fadeIn(1000);



            TurnDownSibling(this);
 



        }

        else if (z == 0) {

            var isHidden = $(this).siblings().children('img').is(':hidden');


 

            $("#trailer" + pan.toString()).fadeOut(900);
            $(this).children().not("#trailer" + pan.toString()).delay(1000).fadeIn(1000);
            $(labid).toggleClass('RLabel').fadeIn(300);



            setTimeout(function () {
                document.getElementById(ifr.toString()).setAttribute("src", "http://www.youtube.com/embed/" + url.toString() + "?stop=1");
            }, 3000);

        }


    });
});





function myMovie(movie, pan) {

  

    var frame = document.createElement("iframe");

    frame.src = "http://www.youtube.com/embed/" + movie.toString() + "?";
    frame.className = "vid";
    frame.id = "trailer" + pan.toString();
    frame.frameborder = "0";

    frame.allowfullscreen = "0";
    document.getElementById(pan.toString()).appendChild(frame);


}


function ShowVid(priceid, cheid,pic,pan,labid) {




    $(priceid).fadeToggle(900);
    $(cheid).fadeToggle(900);
    $("#" + pic.toString()).fadeToggle(900);
    $("#trailer" + pan.toString()).delay(1000).fadeToggle(1000);
    $(labid).toggleClass('RLabel');



}



function ShowImg(priceid, cheid, pic, pan, labid,ifr) {


    $("#trailer" + pan.toString()).fadeToggle(900);
    $("#" + pic.toString()).delay(1000).fadeToggle(1000);
    $(priceid).delay(1000).fadeToggle(1000);
    $(cheid).delay(1000).fadeToggle(1000);
    $(labid).toggleClass('RLabel');
    setTimeout(function () {
        document.getElementById(ifr.toString()).setAttribute("src", "http://www.youtube.com/embed/" + url.toString() + "?stop=1");
    }, 3000);





}


function TurnDownSibling(me) {



   
   
    var isHidden = $(me).siblings().children('img').is(':hidden');

    if (isHidden == true) {
        img = new Array($(me).siblings().find('img:hidden').attr('id'));
            
                    img = "#" + img;
                    fa = $(img).parent().attr('id');
                    fa = "#" + fa;
                    Mv = $(fa).find('iframe').attr('id');
                    mv = "#" + Mv;
                    siburl = $(fa).find('img').attr('class');

                    $(fa).toggleClass('Tfront');
                    $(mv).fadeOut(900);
                    $(fa).children().not(mv).delay(1000).fadeIn(1000);

                    if (Mv != undefined) {
                        setTimeout(function () {
                            document.getElementById(Mv.toString()).setAttribute("src", "http://www.youtube.com/embed/" + siburl.toString() + "?stop=1");
                        }, 3000);
                    
        }


    }

 


}

