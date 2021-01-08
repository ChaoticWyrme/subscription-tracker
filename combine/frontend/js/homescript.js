
let subs = [];

//make the subscription boxes
//async function makesubboxes(){
const makesubboxes = async () => {
    console.log("before");
    let storage = await browser.storage.local.get(["subList"]);

    let subList = storage["subList"] || [];
    console.log(subList);

    document.getElementById("subscriptions").innerHTML = "";

    subs=[];

    let totalprice = 0.0;
    console.log(totalprice);
    if(subList.length > 0){
        for(var k = 0; k < subList.length; k++){
            subs.push({
                name: subList[k].name,
                price: subList[k].price,
                timewatched: k*60
            })
            totalprice += parseFloat(subList[k].price);
            console.log(totalprice);
            document.getElementById("subscriptions").innerHTML += `
                <div class="subscriptionbox" id="${subList[k].name}">
                    <div class="subscriptionleft">
                        <img src="${subList[k].icon}" class="subscriptionicon" width="60" height="60">

                        <p class="subscriptionname">${subList[k].name}</p>
                    </div>
                    <div class="subscriptionright">
                        <p class="subscriptionprice">${subList[k].price}</p>
                        <button type="button" class="close" aria-label="Close" onclick="removesub(${subList[k].name})">
                            <span aria-hidden="true">&times;</span>
                        </button>

                    </div>
            
            `
        }
    }

    let samplenetflix = 
        {
            id: "netflix",
            description: "Netflix Basic",
            url: "www.netflix.com",
            price: 8.99,
            icon: "images/netflixicon.png",
          };
    
          totalprice += parseFloat(samplenetflix.price);
          console.log(totalprice);
          document.getElementById("subscriptions").innerHTML += `
              <div class="subscriptionbox" id="${samplenetflix.id}">
                  <div class="subscriptionleft">
                      <img src="${samplenetflix.icon}" class="subscriptionicon" width="80" height="60">

                      <p class="subscriptionname">${samplenetflix.description}</p>
                  </div>
                  <div class="subscriptionright">
                      <p class="subscriptionprice">${samplenetflix.price}</p>
                      <button type="button" class="close" aria-label="Close" onclick="removesub(${samplenetflix.description})">
                          <span aria-hidden="true">&times;</span>
                      </button>

                  </div>
    `
    subs.push({
        name:"Netflix",
        price:8.99,
        timewatched:500
    })
    document.getElementById("priceplace").innerHTML =  `Total: $${totalprice.toFixed(2)}`;
}
console.log("hello")
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
