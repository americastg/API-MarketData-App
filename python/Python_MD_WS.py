import websocket, msgpack, requests, json

BASE_URL = '<BASE_URL>/api/ws'
TOKEN_URL = '<TOKEN_URL>/connect/token'

token_request = {
    'grant_type': 'client_credentials',  # do not change
    'scope': 'atgapi',    # do not change
    'client_id': '<CLIENT_ID>',
    'client_secret': '<CLIENT_SECRET>'
}

symbols = [ "PETR4", "VALE3" ] # 200 symbols max
updateFreq = 1 # integer, if not set, defaults to 500ms
isFirstMessage = True

def get_token():
    response = requests.post(TOKEN_URL, data=token_request)
    response.raise_for_status()
    return response.json()['access_token']

def on_message(ws, message):
    global isFirstMessage
    if isFirstMessage:
        print(message)
        isFirstMessage = False
        return
    if message == b'\xff':
        ws.send(b'1')
        return
    messageDes = msgpack.unpackb(message, timestamp = 3 ) # to get datetime with timezone info
    print(messageDes)

def on_error(ws, error):
    print("Error:")
    print(error)

def on_open(ws):
    ws.send(json.dumps({ "Token": get_token(), "Symbols": symbols, "UpdateFreq": updateFreq }))

def main():
    websocket.enableTrace(False)
    websocketBaseUrl = BASE_URL.replace('http','ws')
    data = "book"
    endpointAddress = f'{websocketBaseUrl}/{data}'
    ws = websocket.WebSocketApp(endpointAddress,
                                on_open = on_open,
                                on_message = on_message,
                                on_error = on_error)
    ws.on_open = on_open
    ws.run_forever()

if __name__ == "__main__":
    main()
