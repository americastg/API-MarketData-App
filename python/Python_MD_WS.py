import websocket, msgpack, requests

BASE_URL = '<BASE_URL>/api/ws'
TOKEN_URL = '<TOKEN_URL>/connect/token'

token_request = {
    'grant_type': 'client_credentials',  # do not change
    'scope': 'mdapi4',    # do not change
    'client_id': '<CLIENT_ID>',
    'client_secret': '<CLIENT_SECRET>'
}

def get_token():
    response = requests.post(TOKEN_URL, data=token_request)
    response.raise_for_status()
    return response.json()['access_token']

def on_message(ws, message):
    if message == b'\xff':
        ws.send(b'1')
        return
    messageDes = msgpack.unpackb(message, timestamp = 3 ) # to get datetime with timezone info
    print(messageDes)

def on_error(ws, error):
    print("Error:")
    print(error)

def on_open(ws):
    token = get_token()
    ws.send(token)

def main():
    websocket.enableTrace(False)
    symbol = "PETR4"
    websocketBaseUrl = BASE_URL.replace('http','ws')
    data = "book"
    endpointAddress = f'{websocketBaseUrl}/{symbol}/{data}'
    ws = websocket.WebSocketApp(endpointAddress,
                                on_open = on_open,
                                on_message = on_message,
                                on_error = on_error
                                )
    ws.on_open = on_open
    ws.run_forever()

if __name__ == "__main__":
    main()
