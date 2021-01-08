
function makegrid(gridid){
    console.log(gridid);
    if(subs.length > 0){
        document.getElementById(gridid).innerHTML = "";
        
        //header
        /*
        document.getElementById(gridid).innerHTML += `
            <div class="row">
                <div class="col-sm">
                Service
                </div>
                <div class="col-sm">
                Minutes Watched
                </div>
                <div class="col-sm">
                Minutes/Dollar
                </div>
                <div class="col-sm">
                Verdict
                </div>
            </div>
        `;*/

        for(var k = 0; k < subs.length; k++){

            let timewatched = subs[k].timewatched;

            let minperdollar = (parseToFloat(timewatched)/parseToFloat(subList[k].price)).toFixed(2);

            let verdict = "";
            if(minperdollar > 2*10.59){
                verdict = "Dubscription";
            } else if(minperdollar > 10.59){
                verdict = "Subscription";
            } else {
                verdict = "Lscription";
            }

            document.getElementById(gridid).innerHTML += `
                <p>${subs[k].name} (${subs[k].price}): ${timewatched} minutes watched, ${minperdollar} Minutes per Dollar, ${verdict}.
            `

            /*
            document.getElementById(gridid).innerHTML += `
            <div class="row">
                <div class="col-sm">
                ${subList[k].name}
                </div>
                <div class="col-sm">
                ${timewatched}
                </div>
                <div class="col-sm">
                $${minperdollar}
                </div>
                <div class="col-sm">
                Verdict
                </div>
            </div>
        `;*/
            
        }

    }
}

makegrid("gridbox");
