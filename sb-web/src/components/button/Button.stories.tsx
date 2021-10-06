import React from 'react';

import { Button, Table, Thead, Tbody, Tr, Th, Td, IconButton } from '@chakra-ui/react';
import { HiArrowRight as RightIcon } from 'react-icons/hi';
import { IoImageOutline as Image } from 'react-icons/io5';

export default {
    title: 'Components/Button',
};

const Template: any = (args: any): JSX.Element => {
    const buttons = [
        {
            title: 'Primary Call-To-Action',
            comp: <Button>Primary Button</Button>,
        },
        {
            title: 'Primary With Icon',
            comp: <Button rightIcon={<RightIcon />}>Primary Button</Button>,
        },
        {
            title: 'Outline',
            comp: <Button variant="outline">Outline Button</Button>,
        },
        {
            title: 'Outline with Icon',
            comp: (
                <Button variant="outline" rightIcon={<RightIcon />}>
                    Outline Button
                </Button>
            ),
        },
        {
            title: 'Secondary Button',
            comp: <Button colorScheme="gray">Secondary Button</Button>,
        },
        {
            title: 'Secondary Button with Icon',
            comp: (
                <Button colorScheme="gray" leftIcon={<Image />}>
                    Secondary Button
                </Button>
            ),
        },
        {
            title: 'Link Button',
            comp: (
                <Button colorScheme="gray" variant="link">
                    Link Button
                </Button>
            ),
        },
        {
            title: 'Icon Button',
            comp: <IconButton aria-label="icon-button" colorScheme="gray" icon={<Image />} variant="ghost" />,
        },
    ];
    return (
        <Table variant="simple">
            <Thead>
                <Tr>
                    <Th>Variant</Th>
                    <Th>Component</Th>
                </Tr>
            </Thead>
            <Tbody>
                {buttons.map((button) => (
                    <Tr>
                        <Td>{button.title}</Td>
                        <Td>{button.comp}</Td>
                    </Tr>
                ))}
            </Tbody>
        </Table>
    );
};

export const Primary = Template.bind({});