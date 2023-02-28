const msgpck = require("@msgpack/msgpack");
const websocket = require('ws').WebSocket;
const axios = require('axios').default;

const TOKEN_URL = '<TOKEN_URL>/connect/token'
const BASE_ADDRESS = '<BASE_ADDRESS>/api/ws'

// Token auth params
const data = {
    grant_type: 'client_credentials', // do not change
    scope: 'atgapi',   // do not change
    client_id: '<CLIENT_ID>',
    client_secret: '<CLIENT_SECRET>'
}

const updateFreq = 1; // integer, if not set, defaults to 500ms
const symbols = [ 'PETR4', 'VALE3' ]; // 200 symbols max

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

async function run(data) {
    const token = await getToken();
    var urlEndpoint = `${BASE_ADDRESS.replace('http','ws')}/${data}`;
    const ws = new websocket(urlEndpoint);
    var isFirstMessage = true;

    ws.onopen = (event) => {
        ws.send(JSON.stringify({ 'Token': token, 'Symbols': symbols, 'UpdateFreq': updateFreq }));
    };

    ws.onmessage = (event) => {
        if(isFirstMessage)
        {
            console.log(event.data.toString('utf8'));
            isFirstMessage = false;
            return;
        }
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
        console.log(event.message)
    }
}

// book subscription
run('book')

// trades subscription
//run('trades')

// best offers subscription
//run('bestoffers')