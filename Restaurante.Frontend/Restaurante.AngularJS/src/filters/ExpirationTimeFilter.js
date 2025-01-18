export default () => (fecha) => {
    const fechaActual = new Date();
    const fechaActualUTC = new Date(Date.UTC(
        fechaActual.getUTCFullYear(),
        fechaActual.getUTCMonth(),
        fechaActual.getUTCDate(),
        fechaActual.getUTCHours(),
        fechaActual.getUTCMinutes(),
        fechaActual.getUTCSeconds()
    ));
    let fechaBUTC = fechaActualUTC;
    if (fecha && fecha !== '-') {
        try {
            const fechaHoraSplitA = fecha.split(' ');
            const splitFecha = fechaHoraSplitA[0].split('-');
            const splitHora = fechaHoraSplitA[1].split(':');
            fechaBUTC = new Date(
                Date.UTC(
                    splitFecha[2],   
                    splitFecha[1] - 1,  
                    splitFecha[0],   
                    splitHora[0],    
                    splitHora[1],   
                    splitHora[2]     
                )
            );
        } catch (e) {
            console.error("Error al parsear la fecha:", e);
        }
    }else{
        return '-';
    }
    
    
    const diferencia = fechaBUTC - fechaActualUTC;
    
    if (diferencia <= 0) {
        return "-";
    } else {
        let segundos = Math.floor(diferencia / 1000);
        let minutos = Math.floor(segundos / 60); 
        let horas = Math.floor(minutos / 60);
        let dias = Math.floor(horas / 24);

        segundos = segundos % 60;
        minutos = minutos % 60;
        horas = horas % 24;

        return dias.toString().padStart(2, '0') + ':' +
               horas.toString().padStart(2, '0') + ':' +
               minutos.toString().padStart(2, '0') + ':' +
               segundos.toString().padStart(2, '0');
    }
};
