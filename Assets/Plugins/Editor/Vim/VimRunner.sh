#!/bin/bash
#[Configuration]
TERM="termite"

if [ ! -z `vim --serverlist | grep Unity3d` ]; then
    vim --servername Unity3d --remote-silent +$2 $1
else
    $TERM -e "vim --servername Unity3d --remote-silent +$2 $1"
fi
