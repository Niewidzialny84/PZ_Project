# Server
A realtime server for handling incoming traffic about logging in, quizes and other things.

## Requirements
All requirements files are places inside `requirements.txt` file in root folder.

## Running
To run the application API must be up and running. To get access from clients the server application must be run. 

### TCP Server
`python src/server.py`

Running on all interfaces and port 7777. 

## Protocol informations
To implement connection between client and server was used a simple schema for sending data throuout a encrypted TCP stream. Header of this protocol are send inside encrypted data making it application data.

### Header
Header contains informations about used version, the type of the message and a size of the data coming right after.

<table>
    <tr>
        <th>Version</th>
        <th>Type</th>
        <th>Data size</th>
    </tr>
    <tr>
        <td>2 bits</td>
        <td>6 bits</td>
        <td>16 bits</td>
    </tr>
</table>

#### Version
Currently version of the protocol is 1, binary encoded setting it to `01` in the header.

#### Type
It specifies the fields contained inside data that is send after the header. All of them are described inside `protocol.py` file.

#### Data size
It is made up from the converted to bytes data before sending.

### Data
Bytes inside are encoded json documents which after decoding can contain specific fields based on the header type value. The header types and values are describled by the table below:

<table>
    <tr>
        <th>Header Type</th>
        <th>Required Fields</th>
        <th>Optional Fields</th>
        <th>Descrieption</th>
    </tr>
    <tr>
        <td>ACK</td>
        <td></td>
        <td><code>msg: str</code></td>
        <td>Acknowledment, for information about correct data in specific types</td>
    </tr>
    <tr>
        <td>ERR</td>
        <td></td>
        <td><code>msg: str</code></td>
        <td>Any type of error that ocured based on the recived data</td>
    </tr>
    <tr>
        <td>DIS</td>
        <td></td>
        <td><code>msg: str</code></td>
        <td>A disconnect message</td>
    </tr>
    <tr>
        <td>LOG</td>
        <td><code>login: str</code><br><code>password: str</code></td>
        <td></td>
        <td>Login data for verifying the users</td>
    </tr>
    <tr>
        <td>SES</td>
        <td><code>session: int</code></td>
        <td></td>
        <td>Session id, send after succesfull login</td>
    </tr>
    <tr>
        <td>REG</td>
        <td><code>login: str</code><br><code>password: str</code><br><code>email: str</code></td>
        <td></td>
        <td>Register data for creating new accounts</td>
    </tr>
    <tr>
        <td>LIS</td>
        <td><code>quizes: list</code></td>
        <td></td>
        <td>Returns list of avaible quizes</td>
    </tr>
    <tr>
        <td>ALI</td>
        <td></td>
        <td><code>msg: str</code></td>
        <td>Request for quiz list</td>
    </tr>
    <tr>
        <td>QUI</td>
        <td><code>category: str</code></td>
        <td></td>
        <td>Begin quiz request based on category</td>
    </tr>
    <tr>
        <td>QUE</td>
        <td><code>question: str</code><code>answers: list</code><code>correct: str</code></td>
        <td></td>
        <td>Single question from a quiz</td>
    </tr>
    <tr>
        <td>NXT</td>
        <td></td>
        <td><code>msg: str</code></td>
        <td>Request for next question</td>
    </tr>
    <tr>
        <td>END</td>
        <td><code>score: int</code></td>
        <td></td>
        <td>Ending request for quiz with score</td>
    </tr>
    <tr>
        <td>STA</td>
        <td><code>stats: list of dicts</code></td>
        <td></td>
        <td>List of personal stats in pairs e.g<code>{"category": str, "score": int}</code>, Global stats comes in similar list of pairs with change where category is replaced with username</td>
    </tr>
    <tr>
        <td>STR</td>
        <td><code>msg: str</code><code>category: str</code></td>
        <td></td>
        <td>Request for personal stats if category is an empty list [], request for global top 10 when category is set</td>
    </tr>
</table>