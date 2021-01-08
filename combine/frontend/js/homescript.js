

//make the subscription boxes
//async function makesubboxes(){
const makesubboxes = async () => {
    console.log("before");
    let storage = await browser.storage.local.get(["subList"]);

    let subList = storage["subList"] || [];
    console.log(subList);

    document.getElementById("subscriptions").innerHTML = "";

    if(subList.length > 0){
        for(var k = 0; k < subList.length; k++){
            document.getElementById("subscriptions").innerHTML += `
                <div class="subscriptionbox" id="${subList[k].name}">
                    <div class="subscriptionleft">
                        <img src="icon.png" class="subscriptionicon" width="60" height="60">

                        <p class="subscriptionname">${subList[k].name}</p>
                    </div>
                    <div class="subscriptionright">
                        <p class="subscriptionprice">${subList[k].price}</p>
                                            <a href="#"><img src="images/graph.png" class="graphIcon" width="30" height="30"></a>

                        <button type="button" class="close" aria-label="Close" onclick="removesub(${subList[k].name})">
                            <span aria-hidden="true">&times;</span>
                        </button>

                    </div>
            
            `
        }
    } else {
        document.getElementById("subscriptions").innerHTML = `hello`;
    }
    
    
}

makesubboxes();

function isEmpty(obj) {
    for(var prop in obj) {
        if(obj.hasOwnProperty(prop))
            return false;
    }

    return true;
}

const idtodomain = {
    "netflix1":"www.netflix.com"
}

//remove subscription
async function removesub(subid){
    let storage = await browser.storage.local.get(["subList"]);

    let subList = storage["subList"] || [];

    let index = -1
    for(var k = 0; k < subList.length; k++){
        if(subList[k].url === idtodomain[subid]){
            index = k;
            break;
        }
    }
    if (index !== -1) {
        subList.splice(index, 1);
    }

    await browser.storage.local.set({subList});

}
