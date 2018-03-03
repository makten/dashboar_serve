import axios from 'axios';

class Jenkins {

    async statistics(team, environment, pipeline ) {
        var teamx = encodeURI("Eile Mit Weile")
        var pipelinex = encodeURI("Stabilisatie-teststraat-ING")
        var environmentx = "ING";
        const query = `${environmentx}/view/${teamx}/view/${pipelinex}/view/pipeline`;

        const response = await this.performQuery(query);

        return response.data.query.results.channel.item.condition;
    }

    async performQuery(query) {       

        const endpoint = `https://force-ci.topicusfinance.nl/view/${query}/api/json?pretty=true`;
        return await axios.get(endpoint);
    }
}

export default new Jenkins();
