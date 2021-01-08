
async function makegrid(gridid){
    let storage = await browser.storage.local.get(["subList"]);

    let subList = storage["subList"] || [];

    if(subList.length > 0){
        document.getElementById("grid").innerHTML = "";
        
        //header
        document.getElementById("grid").innerHTML += `
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
        `;

        for(var k = 0; k < subList.length; k++){

            let timewatched = 120.0;

            let minperdollar = (parseToFloat(timewatched)/parseToFloat(subList[k].price)).toFixed(2);

            let verdict = "";
            if(minperdollar > 2*10.59){
                verdict = "Dubscription";
            } else if(minperdollar > 10.59){
                verdict = "Subscription";
            } else {
                verdict = "Lscription";
            }

            document.getElementById("grid").innerHTML += `
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
        `;
        }

    }
}

makegrid("gridbox");
