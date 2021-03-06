import React, { FunctionComponent } from 'react';

import { Flex, IconButton, Breadcrumb, BreadcrumbItem, BreadcrumbLink } from '@chakra-ui/react';

import { Boat } from 'boats/Boat.Types';
import { LinkButton } from 'boats/components/link-button/LinkButton';
import { SbSettingsIcon, SbUserGroup } from 'util/icons/Icons';

interface Props {
    boat: Boat;
}

export const Toolbar: FunctionComponent<Props> = ({ boat }) => {
    return (
        <Flex justifyContent="space-between" pb="4" bg="white" alignItems="center">
            <Breadcrumb spacing="8px" fontWeight="semibold" fontSize="xl">
                <BreadcrumbItem>
                    <BreadcrumbLink color="brand.muted">Boats</BreadcrumbLink>
                </BreadcrumbItem>

                <BreadcrumbItem>
                    <BreadcrumbLink isCurrentPage color="teal.600">
                        {boat.name}
                    </BreadcrumbLink>
                </BreadcrumbItem>
            </Breadcrumb>

            <Flex justifyContent="flex-end">
                <LinkButton />
                <IconButton
                    fontSize="2xl"
                    aria-label="users"
                    colorScheme="gray"
                    variant="ghost"
                    icon={<SbUserGroup />}
                />
                <IconButton
                    fontSize="2xl"
                    aria-label="settings"
                    colorScheme="gray"
                    variant="ghost"
                    icon={<SbSettingsIcon />}
                />
            </Flex>
        </Flex>
    );
};
