const msgpck = require("@msgpack/msgpack");
const websocket = require('ws').WebSocket;
const axios = require('axios').default;

const TOKEN_URL = '<TOKEN_URL>/connect/token'
const BASE_ADDRESS = '<BASE_ADDRESS>/api/ws'

// Token auth params
const data = {
    grant_type: 'client_credentials', // do not change
    scope: 'mdapi4',   // do not change
    client_id: '<CLIENT_ID>',
    client_secret: '<CLIENT_SECRET>'
}

async function getToken() {
    let token;
    const query = new URLSearchParams(data);
    await axios.post(TOKEN_URL, query.toString())
        .then(response => {
            console.log('Got access token');
            token = response.data.access_token;
        }).catch(error => console.log('ERROR WHILE TRYING TO GET THE TOKEN: ' + error));

    return token;
}

async function run(symbol, data) {
    const token = await getToken();
    var urlEndpoint = `${BASE_ADDRESS.replace('http','ws')}/${symbol}/${data}`;
    const ws = new websocket(urlEndpoint);

    ws.onopen = (event) => {
        ws.send(token);
    };

    ws.onmessage = (event) => {
		if(event.data[0] == 0xFF)
        {
            ws.send(0xFF)
            return;
        }
        const object = msgpck.decode(event.data);
        console.log(object);
    };

    ws.onerror = (event) => {
        console.log('Error')
        console.log(event.data)
    }
}

// book subscription
run('PETR4', 'book')

// trades subscription
//run('PETR4', 'trades')

// best offers subscription
//run('PETR4', 'bestoffers')