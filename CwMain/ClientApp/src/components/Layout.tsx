import React from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

const Layout = (props: { children: React.ReactNode }) => {
    return (
        <div>
            <NavMenu />
            <Container>
                {props.children}
            </Container>
        </div>
    );
}

Layout.displayName = 'Layout';

export { Layout };
