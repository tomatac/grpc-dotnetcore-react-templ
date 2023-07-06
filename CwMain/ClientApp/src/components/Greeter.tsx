import React, { useState, useEffect } from 'react';
import { GreeterClient } from '../protos-built/GreetServiceClientPb';
import { HelloRequest } from '../protos-built/greet_pb';

const greeter = new GreeterClient('http://192.168.229.128:50001');

type GreeterProps = {
    name: string;
};

const Greeter: React.FC<GreeterProps> = ({ name }) => {
    const [greeting, setGreeting] = useState('');

    useEffect(() => {
        const request = new HelloRequest();
        request.setName(name);

        greeter.sayHello(request, {}, (err, response) => {
            if (err) {
                console.error(`Unexpected error: ${err.message}`);
                return;
            }

            setGreeting(response.getMessage());
        });
    }, [name]);

    return (
        <div>{greeting}</div>
    );
}

export default Greeter;
