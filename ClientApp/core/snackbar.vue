

<script>
  export default {
      props: {
        message: {
          required: true
        },
        actionText: {
          required: true
        },
        actionHandler: {
          required: true
        },
        timeout: {
          required: false,
          default: 2000
        },
        displayWhen: {
          type: 'String'
        }
      },

      mounted() {
        this.$on(this.displayWhen, function(snackbarData) {
          this.show(snackbarData);
        })
      },

      data() {
        return {
          snackbarData: {
            message: this.message,
            timeout: this.timeout,
            actionHandler: this.actionHandler,
            actionText: this.actionText
          }
        };
      },
      methods: {
        show(data) {
          if (data === undefined) {
            data = this.snackbarData;
          }
          this.$el.MaterialSnackbar.showSnackbar(data);
        }
      }
  }
</script>

<template>
  <div>
      <div class="mdl-js-snackbar mdl-snackbar">
        <div class="mdl-snackbar__text"></div>
        <button class="mdl-snackbar__action" type="button"></button>
      </div>

      <button type="button" class="btn btn-default" data-style="toast" data-content="This is a toast! Lorem lipsum dolor sit amet..." data-toggle="snackbar" data-timeout="0">Show toast</button>
  </div>
</template>