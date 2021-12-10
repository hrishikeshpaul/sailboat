import React, { FunctionComponent, useEffect, useState } from 'react';

import { Box, Button, Flex, Text, Heading, Link } from '@chakra-ui/react';

import { Footer } from 'modules/footer/Footer';
import { Routes } from 'router/Router.Types';
import { SbRightArrowIcon } from 'util/icons/Icons';

export enum ErrorCode {
    InviteNotFound = 'SBE1000',
    InviteUnauthorized = 'SBE1001',
    InviteError = 'SBE1002',
    BoatNotFound = 'SBE1100',
    BoatUnauthorized = 'SBE1101',
    BoatError = 'SBE1102',
}

export const getErrorPath = (code: string): string => {
    return `${Routes.Private.Error}?code=${code}`;
};

export const Error: FunctionComponent = () => {
    const [errorState, setErrorState] = useState<{ text: string; heading: string }>({
        text: '',
        heading: '',
    });

    useEffect(() => {
        const urlSearchParams = new URLSearchParams(window.location.search);
        const errorCode: string | null = urlSearchParams.get('code');

        switch (errorCode) {
            case ErrorCode.InviteNotFound:
                setErrorState({
                    heading: 'Invalid invite! :(',
                    text: 'The invite that you are looking for is invalid. Please check the invite link in your email or contact your Captain.',
                });
                break;
            default:
                setErrorState({
                    heading: 'Oops!',
                    text: 'Looks like something went wrong! :(',
                });
        }
    }, []);

    const onHomeRoute = (): void => {
        window.location.href = Routes.Private.Boats;
    };

    return (
        <>
            <Flex justifyContent="center" alignItems="center" w="100%" py="48" px="4">
                <Box textAlign="center">
                    <Heading>{errorState.heading}</Heading>
                    <Text pt="2" fontWeight="normal">
                        {errorState.text}
                    </Text>
                    <Box pt="16">
                        <Flex alignItems="center" justifyContent="center">
                            <Button rightIcon={<SbRightArrowIcon />} onClick={onHomeRoute}>
                                <Text>Back to Home</Text>
                            </Button>
                        </Flex>
                        <Text fontSize="sm" pt="16">
                            Please contact us{' '}
                            <Link color="brand.dark" href={Routes.Whitelisted.Contact}>
                                here
                            </Link>{' '}
                            if the problem persists.
                        </Text>
                    </Box>
                </Box>
            </Flex>
            <Footer />
        </>
    );
};